using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;
using System.ComponentModel.DataAnnotations;

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
        /// Retrieves a count of a promotion.
        /// </summary>
        /// 
        /// <param name="id"></param>
        /// 
        /// <returns>A specific Promotion by Id.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a single promotion by Id.
        /// - Sample request: GET /api/promotions/1
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid ID supplied</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Promotion), 200)]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPromotionById(string id)
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
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Update an existing promotion.
        /// </summary>
        /// 
        /// <param name="promotion">
        /// Pomotion object that needs to be updated.
        /// </param>
        /// 
        /// <returns>An update existing promotion.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return an update existing promotion.
        /// - Sample request: PUT /api/promotions
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid ID supplied</response>
        /// <response code="404">Promotion not found</response>
        /// <response code="405">Validation exception</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Promotion), 200)]
        [Produces("application/json")]
        [HttpPut]
        public async Task<IActionResult> UpdatePromotion([Required]Promotion promotion)
        {
            try
            {
                var response = await _promotionService.GetReponseUpdatedPromotion(promotion);
                if (response.Success)
                {
                    return Ok(response);
                }
                else if (response.StatusCode.Equals(400))
                {
                    return BadRequest(response);
                }
                else if (response.StatusCode.Equals(404))
                {
                    return NotFound(response);
                } 
                else if (response.StatusCode.Equals(405))
                {
                    return StatusCode(405, response.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

            return Ok();
        }
    }
}
