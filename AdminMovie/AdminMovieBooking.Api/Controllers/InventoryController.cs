using AdminMovieBooking.Domain.Models;
using AdminMovieBooking.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminMovieBooking.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/movie/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService) 
        {
            _inventoryService = inventoryService;
        }

        [Route("add")]
        [HttpPost]
        public async Task<ActionResult<string>> AddMovieInventoryDetails(InventoryInputParams inventoryInputParams)
        {
            if (inventoryInputParams == null)
            {
                return NoContent();
            }

            return Ok(await _inventoryService.AddMovieInventoryDetailsAsync(inventoryInputParams));           
        }        
    }
}
