using GameAPI.Data;
using GameAPI.DTOs;
using GameAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Services
{
    public class AchievementService
    {
        private readonly AppDbContext _context;

        public AchievementService(AppDbContext context)
        {
            _context = context;
        }

        // list of all achievements in the system
        public async Task<List<AchievementResponse>> GetAllAchievementsAsync()
        {
            return await _context.Achievements
                .Select(a => new AchievementResponse
                {
                    Id = a.Id,
                    Key = a.Key,
                    Name = a.Name,
                    Description = a.Description,
                    IconUrl = a.IconUrl,
                    Points = a.Points
                })
                .ToListAsync();
        }

        // achievements unlocked by a specific user
        public async Task<List<UserAchievementResponse>> GetUserAchievementsAsync(int userId)
        {
            return await _context.UserAchievements
                .Where(ua => ua.UserId == userId)
                .Include(ua => ua.Achievement)
                .Select(ua => new UserAchievementResponse
                {
                    Id = ua.Achievement.Id,
                    Key = ua.Achievement.Key,
                    Name = ua.Achievement.Name,
                    Description = ua.Achievement.Description,
                    IconUrl = ua.Achievement.IconUrl,
                    Points = ua.Achievement.Points,
                    UnlockedAt = ua.UnlockedAt
                })
                .ToListAsync();
        }

        // unlock an achievement for a user
        public async Task<UserAchievementResponse?> UnlockAchievementAsync(int userId, string achievementKey)
        {
            // find achievement by key
            var achievement = await _context.Achievements
                .FirstOrDefaultAsync(a => a.Key == achievementKey);

            if (achievement == null)
                return null;

            // check if already unlocked
            bool alreadyUnlocked = await _context.UserAchievements
                .AnyAsync(ua => ua.UserId == userId && ua.AchievementId == achievement.Id);

            if (alreadyUnlocked)
                return null;

            // unlock achievement
            var userAchievement = new UserAchievement
            {
                UserId = userId,
                AchievementId = achievement.Id
            };

            _context.UserAchievements.Add(userAchievement);
            await _context.SaveChangesAsync();

            return new UserAchievementResponse
            {
                Id = achievement.Id,
                Key = achievement.Key,
                Name = achievement.Name,
                Description = achievement.Description,
                IconUrl = achievement.IconUrl,
                Points = achievement.Points,
                UnlockedAt = userAchievement.UnlockedAt
            };
        }

        // create a new achievement (Admin only)
        public async Task<AchievementResponse?> CreateAchievementAsync(CreateAchievementRequest request)
        {
            // check if achievement with the same key already exists
            bool exists = await _context.Achievements
                .AnyAsync(a => a.Key == request.Key);

            if (exists)
                return null;

            var achievement = new Achievement
            {
                Key = request.Key,
                Name = request.Name,
                Description = request.Description,
                IconUrl = request.IconUrl,
                Points = request.Points
            };

            _context.Achievements.Add(achievement);
            await _context.SaveChangesAsync();

            return new AchievementResponse
            {
                Id = achievement.Id,
                Key = achievement.Key,
                Name = achievement.Name,
                Description = achievement.Description,
                IconUrl = achievement.IconUrl,
                Points = achievement.Points
            };
        }
    }
}