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
        [HttpGet]
        public async Task<ActionResult<Response<List<AdminManager>>>> GetListAdminWithPagination([FromBody]Pagination pagination)
        {
            var res = await _adminService.GetAllAdminWithPagination(pagination.PageIndex, pagination.PageSize);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> DisableOrEnableAdminAccount(int id)
        {
            var res = await _adminService.DisableOrEnableAdmin(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<Response<string>>> CreateNewAdmin(AdminManager admin)
        {
            var res = await _adminService.CreateNewAdmin(admin);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("count")]
        public async Task<ActionResult<Response<string>>> CountAdmin()
        {
            var res = await _adminService.CountAdmin();
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
    }
}
