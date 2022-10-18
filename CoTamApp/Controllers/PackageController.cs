using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;

namespace CoTamApp.Controllers
{
    [Route("api/packages")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }
        [HttpGet("count/{serviceId}")]
        public async Task<ActionResult<Response<int>>> CountPackageByServiceId(int serviceId)
        {
            var res = await _packageService.CountPackageByServiceId(serviceId);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("service/{serviceId}")]
        public async Task<ActionResult<Response<List<Package>>>> GetAllPackageByEachServiceWithPagination(int serviceId, [FromQuery] int PageIndex, [FromQuery] int PageSize)
        {
            var res = await _packageService.GetAllPackageByEachServiceWithPagination(serviceId, PageIndex, PageSize);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<Package>>> GetPackageById(int id)
        {
            var res = await _packageService.GetPackageById(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> DisableOrEnablePackage(int id)
        {
            try
            {
                var res = await _packageService.DisableOrEnablePackage(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        /// <summary>
        /// Add New Package with information include NumberOfWorker, Duration, and serviceId
        /// </summary>
        /// 
        /// <remarks>
        /// Description: 
        /// - NumberOfWorker, Duration, and serviceId are required when create
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Response<string>>> CreateNewPackage(Package package)
        {
            try
            {
                var res = await _packageService.CreateNewPackage(package);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            { 
                return StatusCode(500, "Internal server error: "+ex.Message);
            }
        }
        /// <summary>
        /// Update Package with information include NumberOfWorker, Duration, and serviceId
        /// </summary>
        /// 
        /// <remarks>
        /// Description: 
        /// - Id, NumberOfWorker, Duration, and serviceId is required when update
        /// </remarks>
        [HttpPut]
        public async Task<ActionResult<Response<Package>>> UpdatePackage(Package package)
        {
            try
            {
                var res = await _packageService.UpdatePackage(package);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
