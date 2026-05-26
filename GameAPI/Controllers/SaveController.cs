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
    public class SaveController : ControllerBase
    {
        private readonly SaveService _saveService;

        public SaveController(SaveService saveService)
        {
            _saveService = saveService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSave()
        {
            var userId = GetUserId();
            var save = await _saveService.GetSaveAsync(userId);

            if (save == null)
                return NotFound(new { message = "Missing game save." });

            return Ok(save);
        }

        [HttpPost]
        [EnableRateLimiting("save")]
        public async Task<IActionResult> SaveGame(SaveGameRequest request)
        {
            var userId = GetUserId();
            var save = await _saveService.SaveGameAsync(userId, request);
            return Ok(save);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSave()
        {
            var userId = GetUserId();
            var success = await _saveService.DeleteSaveAsync(userId);

            if (!success)
                return NotFound(new { message = "Missing game save." });

            return Ok(new { message = "Game save deleted." });
        }

        private int GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(claim!);
        }
    }
}