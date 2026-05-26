using GameAPI.Data;
using GameAPI.DTOs;
using GameAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Services
{
    public class LeaderboardService
    {
        private readonly AppDbContext _context;

        public LeaderboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeaderboardEntryResponse>> GetTopScoresAsync(int count = 10)
        {
            var entries = await _context.LeaderboardEntries
                .Include(l => l.User)
                .OrderByDescending(l => l.Score)
                .Take(count)
                .ToListAsync();

            // Mapujemy na DTO i dodajemy rank
            return entries.Select((entry, index) => new LeaderboardEntryResponse
            {
                Rank = index + 1,
                Username = entry.User.Username,
                Score = entry.Score,
                Level = entry.Level,
                PlayTime = entry.PlayTime,
                AchievedAt = entry.AchievedAt
            }).ToList();
        }

        public async Task<List<LeaderboardEntryResponse>> GetUserScoresAsync(int userId)
        {
            var entries = await _context.LeaderboardEntries
                .Include(l => l.User)
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.Score)
                .ToListAsync();

            return entries.Select((entry, index) => new LeaderboardEntryResponse
            {
                Rank = index + 1,
                Username = entry.User.Username,
                Score = entry.Score,
                Level = entry.Level,
                PlayTime = entry.PlayTime,
                AchievedAt = entry.AchievedAt
            }).ToList();
        }

        public async Task<LeaderboardEntryResponse> AddScoreAsync(int userId, AddScoreRequest request)
        {
            var entry = new LeaderboardEntry
            {
                UserId = userId,
                Score = request.Score,
                Level = request.Level,
                PlayTime = request.PlayTime
            };

            _context.LeaderboardEntries.Add(entry);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FindAsync(userId);

            return new LeaderboardEntryResponse
            {
                Rank = 0, // policzymy osobno jeśli potrzeba
                Username = user!.Username,
                Score = entry.Score,
                Level = entry.Level,
                PlayTime = entry.PlayTime,
                AchievedAt = entry.AchievedAt
            };
        }
    }
}