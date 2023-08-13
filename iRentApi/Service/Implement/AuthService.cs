using AutoMapper;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.Helpers;
using iRentApi.Model.Http.Auth;
using iRentApi.Service.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace iRentApi.Service.Implement
{
    public class AuthService : IRentService, IAuthService
    {
        public AuthService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }

        public async Task<AuthenticateResponse?> Login(string email, string password)
        {
            try
            {
                var user = await Context.Users.Where(user => user.Email == email && user.Password == password).SingleAsync();

                var jwtToken = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken(user);

                user.RefreshToken = refreshToken;
                Context.Update(user);
                Context.SaveChanges();

                return new AuthenticateResponse(user, jwtToken, refreshToken);
            } 
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return null;
            }
        }

        public AuthenticateResponse? RefreshToken(long id, string token)
        {
            var user = Context.Users.SingleOrDefault(u => u.RefreshToken == token);

            if (user == null || user.Id != id) return null;

            var refreshToken = ValidateToken(id, token);

            // return null if token is no longer active
            if (refreshToken ==  null) return null;

            // generate new jwt
            var jwtToken = GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken, null);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken? ValidateToken(long userId, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken;
        }
    }
}
