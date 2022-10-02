using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceResponse;
using Services;
using System.Security.Claims;

namespace CoTamApp.Controllers
{
    [Route("api/[controller]")]
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
    }
}
