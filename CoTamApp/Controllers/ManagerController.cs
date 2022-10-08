using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;

namespace CoTamApp.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Manager")]
    [Route("api/managers")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }
        [HttpGet]
        public async Task<ActionResult<Response<List<AdminManager>>>> GetAllManagerWithPagination([FromBody]Pagination pagination)
        {
            var res = await _managerService.GetAllManagerWithPagination(pagination.PageIndex, pagination.PageSize);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> DisableOrEnableManagerAccount(int id)
        {
            var res = await _managerService.DisableOrEnableManager(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<AdminManager>>> GetManagerById(int id)
        {
            var res = await _managerService.GetManager(id);
            if (!res.Success)
                return NotFound(res);
            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<Response<string>>> CreateNewManager(AdminManager manager)
        {
            var res = await _managerService.CreateNewManager(manager);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("count")]
        public async Task<ActionResult<Response<string>>> CountManager()
        {
            var res = await _managerService.CountManager();
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
    }
}
