using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;
using System.Web.Http.Cors;

namespace CoTamApp.Controllers
{
    [EnableCors(origins: "http://cotam.azurewebsites.net/", headers: "*", methods: "*")]
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("count")]
        public async Task<ActionResult<Response<int>>> CountOrder()
        {
            try
            {
                var res = await _orderService.CountOrder();
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<Response<List<Order>>>> GetListOrderWithPagination([FromQuery] int PageIndex, [FromQuery] int PageSize)
        {
            try
            {
                var res = await _orderService.GetAllOrderWithPagination(PageIndex, PageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<Order>>> GetOrderById(int id)
        {
            try
            {
                var res = await _orderService.GetOrderById(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpDelete("change-order-status/{orderId}")]
        public async Task<ActionResult<Response<string>>> ChangeOrderStatus(int orderId)
        {
            try
            {
                var res = await _orderService.ChangeTheOrderState(orderId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpDelete("cancel-order-status/{orderId}")]
        public async Task<ActionResult<Response<string>>> ChangeToCancleOrder(int orderId)
        {
            try
            {
                var res = await _orderService.ChangeTheOrderStateToCancle(orderId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("order-history/{cusId}")]
        public async Task<ActionResult<Response<List<Order>>>> GetOrderHistoryByCusId(int cusId)
        {
            try
            {
                var res = await _orderService.GetOrdersHistoryByCusId(cusId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("order-pending/{cusId}")]
        public async Task<ActionResult<Response<List<Order>>>> GetOrdersHasStateDangDatByCusId(int cusId)
        {
            try
            {
                var res = await _orderService.GetOrdersHasStateDangDatByCusId(cusId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<Response<List<Order>>>> GetOrderWhenSearching(string searchString, [FromQuery] int PageIndex, [FromQuery] int PageSize)
        {
            var res = await _orderService.SearchOrder(searchString, PageIndex, PageSize);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("search/count/{searchString}")]
        public async Task<ActionResult<Response<int>>> CountOrderWhenSearching(string searchString)
        {
            var res = await _orderService.CountOrdersWhenSearch(searchString);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
    }
}
