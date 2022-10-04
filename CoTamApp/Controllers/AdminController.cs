using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;

namespace CoTamApp.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [Route("api/admins")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<AdminManager>>> GetAdmin_ManagerById(int id)
        {
            var res = await _adminService.GetAdmin_ManagerById(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("page/{page}")]
        public async Task<ActionResult<Response<List<AdminManager>>>> GetListAdminWithPagination(int page)
        {
            var res = await _adminService.GetAllAdminWithPagination(page);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

    }
}
