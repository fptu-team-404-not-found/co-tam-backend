using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;

namespace CoTamApp.Controllers
{
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
                return StatusCode(500, "Internal server error: "+ex.Message);
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
    }
}
