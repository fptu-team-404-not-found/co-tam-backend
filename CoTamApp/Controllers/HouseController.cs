using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace CoTamApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseService _houseService;

        public HouseController(IHouseService houseService)
        {
            _houseService = houseService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet("house/{id}")]
        public async Task<ActionResult<Response<House>>> GetHouseById(int id)
        {
            var res = await _houseService.GetHouseById(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

    }
}
