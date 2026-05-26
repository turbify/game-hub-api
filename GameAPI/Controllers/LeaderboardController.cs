using GameAPI.DTOs;
using GameAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly LeaderboardService _leaderboardService;

        public LeaderboardController(LeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        // Public endpoint – top scores
        [HttpGet("top")]
        public async Task<IActionResult> GetTopScores([FromQuery] int count = 10)
        {
            var scores = await _leaderboardService.GetTopScoresAsync(count);
            return Ok(scores);
        }

        // Secured endpoint - user scores
        [HttpGet("my-scores")]
        [Authorize]
        public async Task<IActionResult> GetMyScores()
        {
            var userId = GetUserId();
            var scores = await _leaderboardService.GetUserScoresAsync(userId);
            return Ok(scores);
        }

        // Secured endpoint - add score
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddScore(AddScoreRequest request)
        {
            var userId = GetUserId();
            var entry = await _leaderboardService.AddScoreAsync(userId, request);
            return Ok(entry);
        }

        // Helper method to extract user ID from JWT claims
        private int GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(claim!);
        }
    }
}