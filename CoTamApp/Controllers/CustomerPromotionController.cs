using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;
using System.Web.Http.Cors;

namespace CoTamApp.Controllers
{
    [EnableCors(origins: "http://cotam.azurewebsites.net/", headers: "*", methods: "*")]
    [Route("api/customer-promotions")]
    [ApiController]
    public class CustomerPromotionController : ControllerBase
    {
        private readonly ICustomerPromotionService _customerPromotionService;

        public CustomerPromotionController(ICustomerPromotionService customerPromotionService)
        {
            _customerPromotionService = customerPromotionService;
        }
        [HttpGet("count/{cusId}")]
        public async Task<ActionResult<Response<string>>> CountCustomerPromotion(int cusId)
        {
            try
            {
                var res = await _customerPromotionService.CountCustomerPromotion(cusId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }


        }
        [HttpGet("customers/{cusId}")]
        public async Task<ActionResult<Response<List<CustomerPromotion>>>> GetListCustomerPromotionWithPagination(int cusId, [FromQuery] int PageIndex, [FromQuery] int PageSize)
        {
            try
            {
                var res = await _customerPromotionService.GetAllCustomerPromotionWithPagination(cusId, PageIndex, PageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> DisableCustomerPromotion(int id)
        {
            try
            {
                var res = await _customerPromotionService.DisableCustomerPromotions(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        /// <summary>
        /// Add New CustomerPromotion with information include CustomerId, PromotionId
        /// </summary>
        /// 
        /// <remarks>
        /// Description: 
        /// - CustomerId, PromotionId are required when create
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Response<string>>> CreateNewCustomerPromotion(CustomerPromotion customerPromotion)
        {
            try
            {
                var res = await _customerPromotionService.CreateNewCustomerPromotion(customerPromotion);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{cusId}")]
        public async Task<ActionResult<Response<CustomerPromotion>>> GetCustomerPromotionByCusId(int cusId)
        {
            try
            {
                var res = await _customerPromotionService.GetCustomerPromotionById(cusId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
