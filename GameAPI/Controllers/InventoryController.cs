using GameAPI.DTOs;
using GameAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace GameAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [EnableRateLimiting("global")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;

        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInventory()
        {
            var userId = GetUserId();
            var items = await _inventoryService.GetInventoryAsync(userId);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(AddItemRequest request)
        {
            var userId = GetUserId();
            var item = await _inventoryService.AddItemAsync(userId, request);
            return Ok(item);
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateItem(int itemId, UpdateItemRequest request)
        {
            var userId = GetUserId();
            var item = await _inventoryService.UpdateItemAsync(userId, itemId, request);

            if (item == null)
                return NotFound(new { message = "The item does not exist or does not belong to you." });

            return Ok(item);
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> RemoveItem(int itemId)
        {
            var userId = GetUserId();
            var success = await _inventoryService.RemoveItemAsync(userId, itemId);

            if (!success)
                return NotFound(new { message = "The item does not exist or does not belong to you." });

            return Ok(new { message = "Item deleted." });
        }

        private int GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(claim!);
        }
    }
}