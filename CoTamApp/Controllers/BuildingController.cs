using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;
using System.Web.Http.Cors;

namespace CoTamApp.Controllers
{
    [EnableCors(origins: "http://cotam.azurewebsites.net/", headers: "*", methods: "*")]
    [Route("api/buildings")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }
        [HttpGet("count")]
        public async Task<ActionResult<Response<string>>> CountBuilding()
        {
            var res = await _buildingService.CountBuilding();
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet]
        public async Task<ActionResult<Response<List<Building>>>> GetListInformationWithPagination([FromQuery] int PageIndex, [FromQuery] int PageSize)
        {
            var res = await _buildingService.GetAllBuildingWithPagination(PageIndex, PageSize);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<Building>>> GetBuildingById(int id)
        {
            var res = await _buildingService.GetBuildingById(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> DisableOrEnableBuilding(int id)
        {
            var res = await _buildingService.DisableOrEnableBuilding(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        /// <summary>
        /// Add New Building with information include Name, AreaId
        /// </summary>
        /// 
        /// <remarks>
        /// Description: 
        /// - Name, AreaId are required when create
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Response<string>>> CreateNewBuilding(Building building)
        {
            var res = await _buildingService.CreateNewBuilding(building);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        /// <summary>
        /// Update Building with information include Name, AreaId
        /// </summary>
        /// 
        /// <remarks>
        /// Description: 
        /// - Name, AreaId are required when update
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<Customer>>> UpdateCustomer(Building building)
        {
            try
            {
                var response = await _buildingService.UpdateBuilding(building);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
