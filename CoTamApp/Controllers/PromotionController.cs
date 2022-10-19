using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Cors;

namespace CoTamApp.Controllers
{
    [EnableCors(origins: "http://cotam.azurewebsites.net/", headers: "*", methods: "*")]
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
        /// <param name="id">
        /// Promotion Id which is needed for updating a promotion.
        /// </param>
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
        /// - Sample request: PUT /api/promotions/{id}
        /// - Sample request body: 
        ///     
        ///       {
        ///           "code": "string",
        ///           "description": "string",
        ///           "value": 0,
        ///           "discount": 0,
        ///           "amount": 0,
        ///           "startDate": "2022-10-05 10:00:30",
        ///           "endDate": "2022-10-05 10:00:30",
        ///       }
        ///     
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid ID supplied</response>
        /// <response code="404">Promotion not found</response>
        /// <response code="422">Validation exception</response>
        /// <response code="500">Internal server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Promotion), 200)]
        [Produces("application/json")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<Promotion>>> UpdatePromotion(string id, [Required][FromBody] Promotion promotion)
        {
            try
            {
                var response = await _promotionService.GetReponseUpdatedPromotion(id, promotion);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Get a list of promotions.
        /// </summary>
        /// 
        /// <returns>A list of promotions.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a list of promotion.
        /// - Sample request: GET /api/promotions
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="404">List of promotion not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Response<List<Promotion>>), 200)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<Response<List<Promotion>>>> GetListPromotions([FromQuery] int PageIndex, [FromQuery] int PageSize)
        {
            try
            {
                var response = await _promotionService.GetReponsePromotions(PageIndex, PageSize);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Create an new promotion.
        /// </summary>
        /// 
        /// <param name="promotion">
        /// Pomotion object that needs to be create.
        /// </param>
        /// 
        /// <returns>A new promotion.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a new promotion.
        /// - Sample request: POST /api/promotions
        ///     
        ///       {
        ///           "code": "string",
        ///           "description": "string",
        ///           "value": 0,
        ///           "discount": 0,
        ///           "amount": 0,
        ///           "startDate": "2022-10-05 10:00:30",
        ///           "endDate": "2022-10-05 10:00:30",
        ///       }
        ///     
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Response<Promotion>), 201)]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult<Response<Promotion>>> CreateAPromotion([Required][FromBody] Promotion promotion)
        {
            try
            {
                var response = await _promotionService.GetResponseCreateAPromotion(promotion);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Change status of a specific promotion.
        /// </summary>
        /// 
        /// <param name="id">
        /// Promotion Id which is needed for deleting a promotion.
        /// </param>
        /// 
        /// <returns>Status change action status.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return change Status action status.
        /// - Sample request: DELETE /api/promotions/1
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid Id supplied</response>
        /// <response code="404">Promotion not found</response>
        /// <response code="500">Internal server error</response>
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<Promotion>>> ChangeStatusPromotion(string id)
        {
            try
            {
                var response = await _promotionService.GetReponseChangeStatusPromotion(id);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /// <summary>
        /// Get the amount of promotions.
        /// </summary>
        /// 
        /// <returns>A number of promotions.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a number of promotions.
        /// - Sample request: GET /api/promotions/count
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="404">List of promotions not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(Response<int>), 200)]
        [Produces("application/json")]
        [HttpGet("count")]
        public async Task<ActionResult<Response<int>>> CountPromotions()
        {
            try
            {
                var response = await _promotionService.GetResponsePromotionNumber();
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
