﻿using BusinessObject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
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
            if(!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet("admins/{id}")]
        public async Task<ActionResult<ServiceResponse<AdminManager>>> GetAdmin(int id)
        {

            return Ok(await _authService.GetAdminManager(id));

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
        [HttpDelete("Logout")]
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
