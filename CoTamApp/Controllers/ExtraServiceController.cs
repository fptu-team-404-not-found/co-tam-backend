using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;
using System.ComponentModel.DataAnnotations;

namespace CoTamApp.Controllers
{
    /// <summary>
    /// Everything about extra-services.
    /// </summary>
    [Route("api/extra-services")]
    [ApiController]
    public class ExtraServiceController : ControllerBase
    {
        private readonly IExtraServiceService _extraServiceService;

        public ExtraServiceController(IExtraServiceService extraServiceService)
        {
            _extraServiceService = extraServiceService;
        }

        /// <summary>
        /// Get a list of all extra-services.
        /// </summary>
        /// 
        /// <returns>A list of all extra-services.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a list of all extra-services.
        /// - Sample request: GET /api/extra-services
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="404">List of extra-services not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Response<List<ExtraService>>), 200)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<Response<List<ExtraService>>>> GetListExtraServices()
        {
            try
            {
                var response = await _extraServiceService.GetReponseExtraServices();
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Get a specific extra-service.
        /// </summary>
        /// 
        /// <param name="id">
        /// Service Id which is needed for finding a extra-service.
        /// </param>
        /// 
        /// <returns>A specific extra-service by Id.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a specific extra-service by Id.
        /// - Sample request: GET /api/extra-services/1
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid Id supplied</response>
        /// <response code="404">Extra-service not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Response<ExtraService>), 200)]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<ExtraService>>> GetExtraServiceById([FromQuery] string id)
        {
            try
            {
                var response = await _extraServiceService.GetReponseExtraServiceById(id);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Create a new extra-service.
        /// </summary>
        /// 
        /// <param name="service">
        /// Extra-service object that needs to be created.
        /// </param>
        /// 
        /// <returns>A new extra-service.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a new extra-service.
        /// - Sample request: POST /api/extra-services
        ///     
        ///       {
        ///           "name": "string",
        ///           "description": "string",
        ///           "price": 0,
        ///           "serviceId": 0
        ///       }
        ///     
        /// </remarks>
        /// 
        /// <response code="201">Successfully</response>
        /// <response code="400">Bad request</response>ư
        /// <response code="422">Validation exception</response>
        /// <response code="500">Internal server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Response<ExtraService>), 201)]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<Response<ExtraService>>> CreateAService([Required][FromBody] ExtraService extraService)
        {
            try
            {
                // TODO: Service is required?
                var response = await _extraServiceService.GetResponseCreateAExtraService(extraService);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // PUT api/<ExtraServiceController>/5
        [Consumes("application/json")]
        [HttpPut("{id}")]
        public void Put([FromQuery] string id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Delete a specific extra-service.
        /// </summary>
        /// 
        /// <param name="id">
        /// Extra-service Id which is needed for deleting a extra-service.
        /// </param>
        /// 
        /// <returns>Delete action status.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return delete action status.
        /// - Sample request: DELETE /api/extra-services/1
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid Id supplied</response>
        /// <response code="404">Extra-service not found</response>
        /// <response code="500">Internal server error</response>
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<ExtraService>>> DeleteExtraService([FromQuery] string id)
        {
            try
            {
                var response = await _extraServiceService.GetReponseDeleteExtraService(id);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
