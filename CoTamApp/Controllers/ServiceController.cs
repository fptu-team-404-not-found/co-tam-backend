using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;
using System.ComponentModel.DataAnnotations;

namespace CoTamApp.Controllers
{
    /// <summary>
    /// Everything about services.
    /// </summary>
    [Route("api/services")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        /// <summary>
        /// Get a list of all services.
        /// </summary>
        /// 
        /// <returns>A list of all services.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a list of all services.
        /// - Sample request: GET /api/services
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="404">List of services not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Response<List<Service>>), 200)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<Response<List<Service>>>> GetListServices()
        {
            try
            {
                var response = await _serviceService.GetReponseServices();
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Get a specific service.
        /// </summary>
        /// 
        /// <param name="id">
        /// Service Id which is needed for finding a service.
        /// </param>
        /// 
        /// <returns>A specific service by Id.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a specific service by Id.
        /// - Sample request: GET /api/services/1
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid Id supplied</response>
        /// <response code="404">Service not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Response<Service>), 200)]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<Service>>> GetServiceById(string id)
        {
            try
            {
                var response = await _serviceService.GetReponseServiceById(id);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Create a new service.
        /// </summary>
        /// 
        /// <param name="service">
        /// Service object that needs to be created.
        /// </param>
        /// 
        /// <returns>A new service.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a new service.
        /// - Sample request: POST /api/services
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
        [ProducesResponseType(typeof(Response<Service>), 201)]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<Response<Service>>> CreateAService([Required][FromBody] Service service)
        {
            try
            {
                var response = await _serviceService.GetResponseCreateAService(service);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Update an existing service.
        /// </summary>
        /// 
        /// <param name="id">
        /// Service Id which is needed for updating a service.
        /// </param>
        /// 
        /// <param name="service">
        /// Service object that needs to be updated.
        /// </param>
        /// 
        /// <returns>An update existing service.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return an update existing service.
        /// - Sample request: PUT /api/services/{id}
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
        /// <response code="404">Service not found</response>
        /// <response code="500">Internal server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Response<Service>), 200)]
        [Produces("application/json")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<Service>>> UpdateService(string id, [Required][FromBody] Service service)
        {
            try
            {
                var response = await _serviceService.GetReponseUpdateService(id, service);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Delete a specific service.
        /// </summary>
        /// 
        /// <param name="id">
        /// Service Id which is needed for deleting a service.
        /// </param>
        /// 
        /// <returns>Delete action status.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return delete action status.
        /// - Sample request: DELETE /api/services/1
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid Id supplied</response>
        /// <response code="404">Service not found</response>
        /// <response code="500">Internal server error</response>
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<Service>>> DeleteService(string id)
        {
            try
            {
                var response = await _serviceService.GetReponseDeleteService(id);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
