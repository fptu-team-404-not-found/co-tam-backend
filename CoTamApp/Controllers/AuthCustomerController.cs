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
    [Route("api/auth-customer")]
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
        [HttpDelete("logout")]
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
        /// <remarks>
        /// Description: 
        /// - Email is required when login
        /// - Name field is needed when your email is not exist and you want to auto register your account
        /// </remarks>
        [HttpGet("customers/login-ver")]
        public async Task<ActionResult<ServiceResponse<string>>> LoginWithCustomerVer2([FromQuery]string email, [FromQuery]string? name)
        {
            var res = await _authCustomerService.LoginWithCustomerVer2(email, name);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
    }
}
