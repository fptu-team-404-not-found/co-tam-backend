using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
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
        /// Get a specific promotion.
        /// </summary>
        /// 
        /// <param name="id">
        /// Promotion Id which is needed for finding a promotion.
        /// </param>
        /// 
        /// <returns>A specific Promotion by Id.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a specific promotion by Id.
        /// - Sample request: GET /api/promotions/1
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid ID supplied</response>
        /// <response code="404">Can not find the promotion by Id</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Promotion), 200)]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<Promotion>>> GetPromotionById(string id)
        {
            try
            {
                var response = await _promotionService.GetReponsePromotionById(id);
                return StatusCode((int)response.StatusCode, response);
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
        ///     
        ///         {
        ///             "id": 0,
        ///             "code": "string",
        ///             "description": "string",
        ///             "value": 0,
        ///             "discount": 0,
        ///             "amount": 0,
        ///             "startDate": "2022-10-05 10:00:30",
        ///             "endDate": "2022-10-05 10:00:30",
        ///         }
        ///     
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid ID supplied</response>
        /// <response code="404">Promotion not found</response>
        /// <response code="422">Validation exception</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Promotion), 200)]
        [Produces("application/json")]
        [HttpPut]
        public async Task<ActionResult<Response<Promotion>>> UpdatePromotion([Required]Promotion promotion)
        {
            try
            {
                var response = await _promotionService.GetReponseUpdatedPromotion(promotion);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
