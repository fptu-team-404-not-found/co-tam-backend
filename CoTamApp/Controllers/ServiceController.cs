using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoTamApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        // GET: api/<ServiceController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
        [ProducesResponseType(typeof(Service), 200)]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(string id)
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

        // POST api/<ServiceController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ServiceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ServiceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
