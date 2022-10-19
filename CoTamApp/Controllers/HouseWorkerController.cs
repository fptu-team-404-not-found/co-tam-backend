using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;
using System.Security.Claims;
using System.Web.Http.Cors;

namespace CoTamApp.Controllers
{
    [EnableCors(origins: "http://cotam.azurewebsites.net/", headers: "*", methods: "*")]
    [Route("api/houseworkers")]
    [ApiController]
    public class HouseWorkerController : ControllerBase
    {
        private readonly IHouseWorkerService _houseWorkerService;

        public HouseWorkerController(IHouseWorkerService houseWorkerService)
        {
            _houseWorkerService = houseWorkerService;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<HouseWorker>>>> GetListHouseWorkerWithPagination([FromQuery] int PageIndex, [FromQuery] int PageSize)
        {
            var res = await _houseWorkerService.GetAllHouseWorkerWithPagination(PageIndex, PageSize);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("count")]
        public async Task<ActionResult<Response<string>>> CountHouseWorker()
        {
            var res = await _houseWorkerService.CountHouseWorker();
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<HouseWorker>>> GetHouseWorkerById(int id)
        {
            var res = await _houseWorkerService.GetHouseWorkerById(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> DisableOrEnableAccountHouseWorker(int id)
        {
            var res = await _houseWorkerService.DisableOrEnableHouseWorkerAccount(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        /*[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Manager")]*/
        [HttpPost]
        public async Task<ActionResult<Response<string>>> CreateNewHouseWorker(HouseWorker houseWorker)
        {
            /*var managerId = int.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));*/
            var res = await _houseWorkerService.CreateNewHouseWorker(houseWorker);
            if (!res.Success)
                return BadRequest(res);
            return StatusCode((int)res.StatusCode, res);
  
        }
        /// <summary>
        /// Update Housworker Account with information include Name, Phone, DateOfBirth, Email, LinkFacebook, Avatar
        /// </summary>
        /// 
        /// <remarks>
        /// Description: 
        /// - Id, Name, Email of Admin are required when update
        /// </remarks>
        [HttpPut]
        public async Task<ActionResult<Response<string>>> UpdateHouseWorker(HouseWorker houseWorker)
        {
            var res = await _houseWorkerService.UpdateHouseWorker(houseWorker);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
    }
}
