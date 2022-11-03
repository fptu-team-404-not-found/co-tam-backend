using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;

namespace CoTamApp.Controllers
{
    [Route("api/work-in-order")]
    [ApiController]
    public class WorkInOrderController : ControllerBase
    {
        private readonly IWorkerInOrderService _workerInOrderService;

        public WorkInOrderController(IWorkerInOrderService workerInOrderService)
        {
            _workerInOrderService = workerInOrderService;
        }
        [HttpGet]
        public async Task<ActionResult<Response<List<WorkerInOrder>>>> GetAllWorkInOrderWithPagination([FromQuery] int PageIndex, [FromQuery] int PageSize)
        {
            
            try
            {
                var res = await _workerInOrderService.GetAllWorkInOrderWithPagination(PageIndex, PageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("count")]
        public async Task<ActionResult<Response<int>>> CountWorkInOrder()
        {
            try
            {
                var response = await _workerInOrderService.CountWorkerInOrder();
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        /// <summary>
        /// Add Work In Order with information include houseworkerId, orderId, rating
        /// </summary>
        /// 
        /// <remarks>
        /// Description: 
        /// - houseworkerId, orderId are required when adding
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Response<int>>> CreateNewWorkInOrder([FromQuery]int orderId, [FromQuery]int houseworkerId, [FromBody] WorkerInOrder workerInOrder)
        {
            try
            {
                var res = await _workerInOrderService.CreateNewWorkInOder(orderId, houseworkerId, workerInOrder);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<WorkerInOrder>>> GetWorkInOrderById(int id)
        {
            try
            {
                var res = await _workerInOrderService.GetWorkerInOrderById(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> RemoveWorkInOrders(int id)
        {
            try
            {
                var res = await _workerInOrderService.RemoveWorkInOrder(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}/{rating}")]
        public async Task<ActionResult<Response<string>>> UpdateRating(int id, int rating)
        {
            try
            {
                var res = await _workerInOrderService.UpdateRating(id, rating);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("houseworkers/{houseworkerId}")]
        public async Task<ActionResult<Response<List<WorkerInOrder>>>> GetWorkerInOrderByHouseworkerId(int houseworkerId)
        {
            try
            {
                var res = await _workerInOrderService.GetWorkerInOrdersByHouseworkerId(houseworkerId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
