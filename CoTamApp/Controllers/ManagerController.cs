using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;
using System.Web.Http.Cors;

namespace CoTamApp.Controllers
{
    [EnableCors(origins: "http://cotam.azurewebsites.net/", headers: "*", methods: "*")]
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
        public async Task<ActionResult<Response<List<AdminManager>>>> GetAllManagerWithPagination([FromQuery] int PageIndex, [FromQuery] int PageSize)
        {
            var res = await _managerService.GetAllManagerWithPagination(PageIndex, PageSize);
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
        /// <summary>
        /// Update Manger Account with information include Name, Phone, DateOfBirth, Email, LinkFacebook, Avatar
        /// </summary>
        /// 
        /// <remarks>
        /// Description: 
        /// - Id, Name, Email of Manager are required when update
        /// </remarks>
        [HttpPut]
        public async Task<ActionResult<Response<string>>> UpdateManager(AdminManager manager)
        {
            var res = await _managerService.UpdateManager(manager);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<Response<List<AdminManager>>>> GetManagerWhenSearching(string searchString, [FromQuery] int PageIndex, [FromQuery] int PageSize)
        {
            var res = await _managerService.SearchAccount(searchString, PageIndex, PageSize);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("search/count/{searchString}")]
        public async Task<ActionResult<Response<int>>> CountManagerWhenSearching(string searchString)
        {
            var res = await _managerService.CountManagerWhenSearch(searchString);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
    }
}
