using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceResponse;
using Services.IServices;
using System.Security.Claims;
using System.Web.Http.Cors;

namespace CoTamApp.Controllers
{
    [EnableCors(origins: "http://cotam.azurewebsites.net/", headers: "*", methods: "*")]
    [Route("api/auth-houseworker")]
    [ApiController]
    public class AuthHouseworkerController : ControllerBase
    {
        private readonly IAuthHouseworkerService _authHouseworkerService;

        public AuthHouseworkerController(IAuthHouseworkerService authHouseworkerService)
        {
            _authHouseworkerService = authHouseworkerService;
        }
        [Authorize]
        [HttpGet("houseworkers/login")]
        public async Task<ActionResult<ServiceResponse<string>>> LoginWithHouseworker()
        {
            string email = this.User.FindFirstValue(ClaimTypes.Email);
            string name = this.User.FindFirstValue(ClaimTypes.Name);
            var res = await _authHouseworkerService.LoginWithHouseworker(email, name);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("logout")]
        public async Task<ActionResult<ServiceResponse<string>>> Logout()
        {
            string rawUserId = this.User.FindFirstValue((ClaimTypes.NameIdentifier));

            if (Convert.ToInt32(rawUserId) == 0)
            {
                return Unauthorized();
            }

            var result = await _authHouseworkerService.Logout(Convert.ToInt32(rawUserId));

            return Ok(result);
        }
    }
}
