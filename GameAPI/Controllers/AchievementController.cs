using GameAPI.DTOs;
using GameAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AchievementController : ControllerBase
    {
        private readonly AchievementService _achievementService;

        public AchievementController(AchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        // Publiczny – wszyscy mogą zobaczyć listę achievementów
        [HttpGet]
        public async Task<IActionResult> GetAllAchievements()
        {
            var achievements = await _achievementService.GetAllAchievementsAsync();
            return Ok(achievements);
        }

        // Chroniony – achievementy zalogowanego gracza
        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyAchievements()
        {
            var userId = GetUserId();
            var achievements = await _achievementService.GetUserAchievementsAsync(userId);
            return Ok(achievements);
        }

        // Chroniony – odblokuj achievement (wywołuje gra)
        [HttpPost("unlock/{achievementKey}")]
        [Authorize]
        public async Task<IActionResult> UnlockAchievement(string achievementKey)
        {
            var userId = GetUserId();
            var result = await _achievementService.UnlockAchievementAsync(userId, achievementKey);

            if (result == null)
                return BadRequest(new { message = "Achievement nie istnieje lub już odblokowany." });

            return Ok(result);
        }

        // Chroniony – utwórz achievement
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAchievement(CreateAchievementRequest request)
        {
            var result = await _achievementService.CreateAchievementAsync(request);

            if (result == null)
                return BadRequest(new { message = "Achievement z tym kluczem już istnieje." });

            return Ok(result);
        }

        private int GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(claim!);
        }
    }
}