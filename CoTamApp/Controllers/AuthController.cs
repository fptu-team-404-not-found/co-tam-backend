using BusinessObject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceResponse;
using Services.IServices;
using System.Security.Claims;
using System.Web.Http.Cors;

namespace CoTamApp.Controllers
{
    /*[EnableCors(origins: "http://cotam.azurewebsites.net/", headers: "*", methods: "*")]*/
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [Authorize]
        [HttpGet("admin-manager/login")]
        public async Task<ActionResult<ServiceResponse<string>>> LoginWithAdminOrManager()
        {
            string email = this.User.FindFirstValue(ClaimTypes.Email);
            string name = this.User.FindFirstValue(ClaimTypes.Name);
            var res = await _authService.LoginWithAdminManager(email, name);
            HttpContext.Session.SetString("AccessToken", res.Data.ToString());
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        
        [HttpGet("admin-manager/login-ver")]
        public async Task<ActionResult<ServiceResponse<string>>> LoginWithAdminManagerVer2([FromQuery] string email)
        {
            var res = await _authService.LoginWithAdminManagerVer2(email);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [Authorize]
        [HttpGet("admin-manager/get-access-token")]
        public async Task<ActionResult<ServiceResponse<string>>> GetAccessToken()
        {
            var res = HttpContext.Session.GetString("AccessToken");
            return Ok(res);
        }
        /*[Authorize]
        [HttpPost("Renew")]
        public async Task<ActionResult<ServiceResponse<string>>> RenewToken(TokenModel model)
        {
            var response = await _authService.RenewToken(model);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }*/
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("logout")]
        public async Task<ActionResult<ServiceResponse<string>>> Logout()
        {
            string rawUserId = this.User.FindFirstValue((ClaimTypes.NameIdentifier));

            if (Convert.ToInt32(rawUserId) == 0)
            {
                return Unauthorized();
            }

            var result = await _authService.Logout(Convert.ToInt32(rawUserId));

            return Ok(result);
        }
    }
}
