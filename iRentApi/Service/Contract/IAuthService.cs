using Domain.Model.Entity;
using iRentApi.Model.Http.Auth;

namespace iRentApi.Service.Contract
{
    public interface IAuthService : IService
    {
        public Task<AuthenticateResponse?> Login(string email, string password);
        public AuthenticateResponse? RefreshToken(string token);
    }
}
