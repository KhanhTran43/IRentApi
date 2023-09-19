using AutoMapper;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.Helpers;
using iRentApi.Model.Http.Auth;
using iRentApi.Service.Database.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace iRentApi.Service.Database.Implement
{
    public class AuthService : IAuthService
    {
        public AuthService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }

        public override async Task<AuthenticateResponse?> Login(string email, string password)
        {
            try
            {
                var user = await base.Context.Users.Where(user => user.Email == email && user.Password == password).SingleAsync();

                var jwtToken = GenerateAccessToken(user);
                var refreshToken = GenerateRefreshToken(user);

                user.RefreshToken = refreshToken;
                base.Context.Update(user);
                base.Context.SaveChanges();

                return new AuthenticateResponse(user, jwtToken, refreshToken);
            }
            catch (System.Exception ex)
            {
                Console.Error.WriteLine(ex);
                return null;
            }
        }

        public override async Task<AuthenticateResponse?> RefreshToken(string token)
        {
            var user = await Context.Users.SingleOrDefaultAsync(u => u.RefreshToken == token);

            if (user == null) return null;

            var refreshToken = ValidateToken(token);

            // return null if token is no longer active
            if (refreshToken == null) return null;

            var userIdFromToken = refreshToken.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;

            // return null if the id not match
            if (userIdFromToken == null || user.Id != long.Parse(userIdFromToken)) return null;

            // generate new jwt
            var jwtToken = GenerateAccessToken(user);

            return new AuthenticateResponse(user, jwtToken, null);
        }

        public override async Task RevokeToken(string refreshToken)
        {
            var user = await Context.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null) return;

            user.RefreshToken = null;

            Context.SaveChanges();
        }

        private string GenerateAccessToken(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
            };
            return GenerateJwtToken(tokenDescriptor);
        }

        private string GenerateRefreshToken(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddYears(1),
            };
            return GenerateJwtToken(tokenDescriptor);
        }

        private string GenerateJwtToken(SecurityTokenDescriptor tokenDescriptor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken? ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(base.AppSettings.Secret);
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
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return null;
            }
        }
    }
}
