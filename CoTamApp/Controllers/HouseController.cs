using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.IServices;

namespace CoTamApp.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseService _houseService;

        public HouseController(IHouseService houseService)
        {
            _houseService = houseService;
        }

        /// <summary>
        /// Get a specific House.
        /// </summary>
        /// 
        /// <param name="id"></param>
        /// <param name="customerId"></param>
        /// 
        /// <returns>A specific House by Id.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a single house by Id.
        /// - Sample request: GET /api/customers/1/houses/1
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid ID supplied</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(House), 200)]
        [Produces("application/json")]
        [HttpGet("{customerId}/houses/{id}")]
        public async Task<ActionResult<Response<House>>> GetHouseById(string id, string customerId)
        {
            try
            {
                var res = await _houseService.GetHouseById(id);
                if (!res.Success)
                {
                    return BadRequest(res);
                }

                return Ok(res);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get a House List by customerId.
        /// </summary>
        /// 
        /// <param name="customerId"></param>
        /// 
        /// <returns>A House List by Customer Id.</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Return a house list by customer id.
        /// - Sample request: GET /api/customers/1/houses
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid ID supplied</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(House), 200)]
        [Produces("application/json")]
        [HttpGet("{customerId}/houses")]
        public async Task<ActionResult<Response<List<House>>>> GetHouseListByCustomerId(string customerId, [FromQuery] int PageIndex, [FromQuery] int PageSize)
        {
            try
            {
                var res = await _houseService.GetHouseListByCustomerId(customerId, PageIndex, PageSize);
                if (!res.Success)
                {
                    return BadRequest(res);
                }

                return Ok(res);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Add a House List for a customer.
        /// </summary>
        /// 
        /// <param name="house"></param>
        /// 
        /// <returns>The added house</returns>
        /// 
        /// <remarks>
        /// Description: 
        /// - Add a House List for a customer.
        /// - Sample request: POST /api/customers/1/houses
        /// </remarks>
        /// 
        /// <response code="200">Successfully</response>
        /// <response code="400">If Invalid ID supplied</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(House), 200)]
        [Produces("application/json")]
        [HttpPost("{house.customerId}/houses")]
        public async Task<ActionResult<Response<House>>> AddHouseForCustomer(House house)
        {
            try
            {
                var res = await _houseService.AddHouseForCustomer(house);
                if (!res.Success)
                {
                    return BadRequest(res);
                }

                return Ok(res);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{house.customerId}/houses/{house.Id}")]
        public async Task<ActionResult<Response<House>>> UpdateHouseForCustomer(House house)
        {
            return Ok();
        }

        [HttpDelete("{house.customerId}/houses/{house.Id}")]
        public async Task<ActionResult<Response<House>>> DeleteHouseForCustomer(House house)
        {
            return Ok();
        }

        [HttpGet("{customerId}/houses/count")]
        public async Task<ActionResult<Response<House>>> CountHouseForCustomer(int customerId)
        {
            try
            {
                var res = await _houseService.CountHousesByCustomerId(customerId);
                if (!res.Success)
                {
                    return BadRequest(res);
                }

                return Ok(res);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
