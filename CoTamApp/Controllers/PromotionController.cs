using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;

namespace CoTamApp.Controllers
{
    [Route("api/promotions")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        /// <summary>
        /// Get a specific Promotion.
        /// </summary>
        /// 
        /// <param name="id"></param>
        /// 
        /// <returns>A specific Promotion by Id.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a single promotion by Id.
        /// - Sample request: GET /api/promotion/1
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid ID supplied</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Promotion), 200)]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPromotionById(int id)
        {
            try
            {
                var response = await _promotionService.GetReponsePromotionById(id);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            } 
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
