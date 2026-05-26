using GameAPI.Data;
using GameAPI.DTOs;
using GameAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Services
{
    public class SaveService
    {
        private readonly AppDbContext _context;

        public SaveService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SaveGameResponse?> GetSaveAsync(int userId)
        {
            var save = await _context.GameSaves
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (save == null)
                return null;

            return MapToResponse(save);
        }

        public async Task<SaveGameResponse> SaveGameAsync(int userId, SaveGameRequest request)
        {
            var existingSave = await _context.GameSaves
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (existingSave != null)
            {
                // update existing save if it exists
                existingSave.SaveData = request.SaveData;
                existingSave.CurrentLevel = request.CurrentLevel;
                existingSave.CurrentScore = request.CurrentScore;
                existingSave.TotalPlayTime = request.TotalPlayTime;
                existingSave.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return MapToResponse(existingSave);
            }

            // create new save if it doesn't exist
            var save = new GameSave
            {
                UserId = userId,
                SaveData = request.SaveData,
                CurrentLevel = request.CurrentLevel,
                CurrentScore = request.CurrentScore,
                TotalPlayTime = request.TotalPlayTime
            };

            _context.GameSaves.Add(save);
            await _context.SaveChangesAsync();
            return MapToResponse(save);
        }

        public async Task<bool> DeleteSaveAsync(int userId)
        {
            var save = await _context.GameSaves
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (save == null)
                return false;

            _context.GameSaves.Remove(save);
            await _context.SaveChangesAsync();
            return true;
        }

        private SaveGameResponse MapToResponse(GameSave save)
        {
            return new SaveGameResponse
            {
                Id = save.Id,
                SaveData = save.SaveData,
                CurrentLevel = save.CurrentLevel,
                CurrentScore = save.CurrentScore,
                TotalPlayTime = save.TotalPlayTime,
                SavedAt = save.SavedAt,
                UpdatedAt = save.UpdatedAt
            };
        }
    }
}