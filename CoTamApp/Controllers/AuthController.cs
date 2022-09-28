﻿using CoTamApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [Authorize]
        [HttpGet("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> LoginWithAdminOrManager()
        {
            string email = this.User.FindFirstValue(ClaimTypes.Email);
            string name = this.User.FindFirstValue(ClaimTypes.Name);
            var res = await _authService.LoginWithAdminManager(email, name);
            if(!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        
        [HttpGet("admin/{id}")]

        public async Task<ActionResult<ServiceResponse<AdminManager>>> GetAdmin(int id)
        {
           
            return Ok(await _authService.GetAdminManager(id));
        }

    }
}
