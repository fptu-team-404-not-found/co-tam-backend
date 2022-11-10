using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Cors;

namespace CoTamApp.Controllers
{
    /// <summary>
    /// Everything about areas.
    /// </summary>
    [EnableCors(origins: "http://cotam.azurewebsites.net/", headers: "*", methods: "*")]
    [Route("api/areas")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        /// <summary>
        /// Get a list of areas.
        /// </summary>
        /// 
        /// <returns>A list of areas.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a list of areas.
        /// - Sample request: GET /api/areas
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="404">List of areas not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Response<List<Area>>), 200)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<Response<List<Area>>>> GetListAreas([FromQuery] int PageIndex, [FromQuery] int PageSize)
        {
            try
            {
                var response = await _areaService.GetReponseAreas(PageIndex, PageSize);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Get a specific area.
        /// </summary>
        /// 
        /// <param name="id">
        /// Area Id which is needed for finding a area.
        /// </param>
        /// 
        /// <returns>A specific area by Id.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a specific area by Id.
        /// - Sample request: GET /api/areas/1
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid Id supplied</response>
        /// <response code="404">Area not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Response<Area>), 200)]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<Area>>> GetAreaById(string id)
        {
            try
            {
                var response = await _areaService.GetReponseAreaById(id);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Create a new area.
        /// </summary>
        /// 
        /// <param name="area">
        /// Area object that needs to be created.
        /// </param>
        /// 
        /// <returns>A new area.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a new area.
        /// - Sample request: POST /api/areas
        ///     
        ///       {
        ///           "name": "string",
        ///           "description": "string",
        ///           "price": 0
        ///       }
        ///     
        /// </remarks>
        /// 
        /// <response code="201">Successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Response<Area>), 201)]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<Response<Area>>> CreateAnArea([Required][FromBody] Area area)
        {
            try
            {
                var response = await _areaService.GetResponseCreateAnArea(area);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Update an existing area.
        /// </summary>
        /// 
        /// <param name="id">
        /// Area Id which is needed for updating a area.
        /// </param>
        /// 
        /// <param name="area">
        /// Area object that needs to be updated.
        /// </param>
        /// 
        /// <returns>An update existing area.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return an update existing area.
        /// - Sample request: PUT /api/areas/{id}
        /// - Sample request body: 
        ///     
        ///       {
        ///           "name": "string",
        ///           "description": "string",
        ///           "price": 0
        ///       }
        ///     
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid ID supplied</response>
        /// <response code="404">Area not found</response>
        /// <response code="500">Internal server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Response<Area>), 200)]
        [Produces("application/json")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<Area>>> UpdateArea(string id, [Required][FromBody] Area area)
        {
            try
            {
                var response = await _areaService.GetReponseUpdateArea(id, area);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Change status of a specific area.
        /// </summary>
        /// 
        /// <param name="id">
        /// Area Id which is needed for deleting a area.
        /// </param>
        /// 
        /// <returns>Status change action status.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return change Status action status.
        /// - Sample request: DELETE /api/areas/1
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid Id supplied</response>
        /// <response code="404">Area not found</response>
        /// <response code="500">Internal server error</response>
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<Area>>> ChangeStatusArea(string id)
        {
            try
            {
                var response = await _areaService.GetReponseChangeStatusArea(id);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Get the amount of areas.
        /// </summary>
        /// 
        /// <returns>A number of areas.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a number of areas.
        /// - Sample request: GET /api/areas/count
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="404">List of areas not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Response<int>), 200)]
        [Produces("application/json")]
        [HttpGet("count")]
        public async Task<ActionResult<Response<int>>> CountArea()
        {
            try
            {
                var response = await _areaService.GetResponseAreaNumber();
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<Response<List<Area>>>> GetAreaWhenSearching(string searchString, [FromQuery] int PageIndex, [FromQuery] int PageSize)
        {
            var res = await _areaService.SearchArea(searchString, PageIndex, PageSize);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("search/count/{searchString}")]
        public async Task<ActionResult<Response<int>>> CountAreanWhenSearching(string searchString)
        {
            var res = await _areaService.CountAreasWhenSearch(searchString);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
    }
}
