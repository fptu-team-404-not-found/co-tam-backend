using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;

namespace CoTamApp.Controllers
{
    [Route("api/informations")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly IInformationService _informationService;

        public InformationController(IInformationService informationService)
        {
            _informationService = informationService;
        }
        [HttpGet("count")]
        public async Task<ActionResult<Response<string>>> CountInformation()
        {
            var res = await _informationService.CountInformation();
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet]
        public async Task<ActionResult<Response<List<Information>>>> GetListInformationWithPagination([FromBody] Pagination pagination)
        {
            var res = await _informationService.GetAllInformationWithPagination(pagination.PageIndex, pagination.PageSize);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<Information>>> GetInformationById(int id)
        {
            var res = await _informationService.GetInformationById(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        /// <summary>
        /// Add New Information with information include Name, Description
        /// </summary>
        /// 
        /// <remarks>
        /// Description: 
        /// - Name is required when create
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Response<string>>> CreateNewInformation(Information information)
        {
            var res = await _informationService.CreateNewInformation(information);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> DisableOrEnableInformation(int id)
        {
            var res = await _informationService.DisableOrEnableInformation(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpPut]
        public async Task<ActionResult<Response<Information>>> UpdatePromotion(Information information)
        {
            try
            {
                var response = await _informationService.UpdateInformation(information);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
