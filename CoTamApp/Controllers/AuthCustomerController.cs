using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceResponse;
using Services.IServices;
using System.Security.Claims;

namespace CoTamApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthCustomerController : ControllerBase
    {
        private readonly IAuthCustomerService _authCustomerService;

        public AuthCustomerController(IAuthCustomerService authCustomerService)
        {
            _authCustomerService = authCustomerService;
        }
        [Authorize]
        [HttpGet("customers/login")]
        public async Task<ActionResult<ServiceResponse<string>>> LoginWithCustomer()
        {
            string email = this.User.FindFirstValue(ClaimTypes.Email);
            string name = this.User.FindFirstValue(ClaimTypes.Name);
            var res = await _authCustomerService.LoginWithCustomer(email, name);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("Logout")]
        public async Task<ActionResult<ServiceResponse<string>>> Logout()
        {
            string rawUserId = this.User.FindFirstValue((ClaimTypes.NameIdentifier));

            if (Convert.ToInt32(rawUserId) == 0)
            {
                return Unauthorized();
            }

            var result = await _authCustomerService.Logout(Convert.ToInt32(rawUserId));

            return Ok(result);
        }
    }
}
