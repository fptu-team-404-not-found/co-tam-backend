using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;

namespace CoTamApp.Controllers
{
    [Route("api/worker-tag")]
    [ApiController]
    public class WorkerTagController : ControllerBase
    {
        private readonly IWorkerTagService _workerTagService;

        public WorkerTagController(IWorkerTagService workerTagService)
        {
            _workerTagService = workerTagService;
        }
        [HttpGet]
        public async Task<ActionResult<Response<List<WorkerTag>>>> GetAllWorkerTagWithPagination([FromQuery] int PageIndex, [FromQuery] int PageSize)
        {

            try
            {
                var res = await _workerTagService.GetAllWorkerTagWithPagination(PageIndex, PageSize);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("count")]
        public async Task<ActionResult<Response<int>>> CountWorkerTag()
        {
            try
            {
                var response = await _workerTagService.CountWorkerTag();
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<WorkerTag>>> GetWorkerTagById(int id)
        {
            try
            {
                var response = await _workerTagService.GetWorkerTagById(id);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("/houseworkers/{houseworkerId}")]
        public async Task<ActionResult<Response<List<WorkerTag>>>> GetWorkerTagsByHouseworkerId(int houseworkerId)
        {
            try
            {
                var response = await _workerTagService.GetWorkerTagsByHouseworkerId(houseworkerId);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> RemoveWorkerTag(int id)
        {
            try
            {
                var response = await _workerTagService.RemoveWorkerTag(id);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        /// <summary>
        /// Add WorkerTag with information include name, houseworkerId
        /// </summary>
        /// 
        /// <remarks>
        /// Description: 
        /// - name, houseworkerId are required when adding
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Response<WorkerTag>>> CreateNewWorkerTag(WorkerTag workerTag)
        {
            try
            {
                var response = await _workerTagService.CreateNewWorkerTag(workerTag);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        /// <summary>
        /// Update Worker with information include name, houseworkerId
        /// </summary>
        /// 
        /// <remarks>
        /// Description: 
        /// - id, houseworkerId are required when updating
        /// </remarks>
        [HttpPut]
        public async Task<ActionResult<Response<WorkerTag>>> UpdateWorkerTag(WorkerTag workerTag)
        {
            try
            {
                var response = await _workerTagService.UpdateWorkerTag(workerTag);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("add")]
        public async Task<ActionResult<Response<List<WorkerTag>>>> GetWorkerTagWhenAdd()
        {
            try
            {
                var response = await _workerTagService.GetWorkerTag();
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
