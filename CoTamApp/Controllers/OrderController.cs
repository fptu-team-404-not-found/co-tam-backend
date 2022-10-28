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
    }
}
