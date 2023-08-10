using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.Service.Contract;
using iRentApi.Service.Implement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : IRentController
    {
        public AuthController(IServiceWrapper serviceWrapper) : base(serviceWrapper)
        {
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInfo loginInfo)
        {
            if (loginInfo != null) {
                var response =  await Service.AuthService.Login(loginInfo.Email, loginInfo.Password);

                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                SetTokenCookie(response.RefreshToken);

                return Ok(response);
            } else return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("refresh-token/{id}")]
        public IActionResult RefreshToken([FromRoute] long id)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if(refreshToken != null)
            {
                var response = Service.AuthService.RefreshToken(id, refreshToken);

                if(response == null) return BadRequest("Invalid refresh token");

                SetTokenCookie(response.RefreshToken);
                return Ok(response);
            } else
            {
                return BadRequest("Invalid refresh token");
            }
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
