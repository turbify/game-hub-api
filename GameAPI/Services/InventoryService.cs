using GameAPI.Data;
using GameAPI.DTOs;
using GameAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Services
{
    public class InventoryService
    {
        private readonly AppDbContext _context;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<InventoryItemResponse>> GetInventoryAsync(int userId)
        {
            return await _context.InventoryItems
                .Where(i => i.UserId == userId)
                .Select(i => new InventoryItemResponse
                {
                    Id = i.Id,
                    ItemKey = i.ItemKey,
                    Quantity = i.Quantity,
                    MetaData = i.MetaData,
                    ObtainedAt = i.ObtainedAt
                })
                .ToListAsync();
        }

        public async Task<InventoryItemResponse?> AddItemAsync(int userId, AddItemRequest request)
        {
            // Sprawdź czy gracz już ma ten przedmiot
            var existingItem = await _context.InventoryItems
                .FirstOrDefaultAsync(i => i.UserId == userId && i.ItemKey == request.ItemKey);

            if (existingItem != null)
            {
                // Jeśli ma – zwiększ ilość
                existingItem.Quantity += request.Quantity;
                await _context.SaveChangesAsync();

                return new InventoryItemResponse
                {
                    Id = existingItem.Id,
                    ItemKey = existingItem.ItemKey,
                    Quantity = existingItem.Quantity,
                    MetaData = existingItem.MetaData,
                    ObtainedAt = existingItem.ObtainedAt
                };
            }

            // Jeśli nie ma – dodaj nowy przedmiot
            var item = new InventoryItem
            {
                UserId = userId,
                ItemKey = request.ItemKey,
                Quantity = request.Quantity,
                MetaData = request.MetaData
            };

            _context.InventoryItems.Add(item);
            await _context.SaveChangesAsync();

            return new InventoryItemResponse
            {
                Id = item.Id,
                ItemKey = item.ItemKey,
                Quantity = item.Quantity,
                MetaData = item.MetaData,
                ObtainedAt = item.ObtainedAt
            };
        }

        public async Task<InventoryItemResponse?> UpdateItemAsync(int userId, int itemId, UpdateItemRequest request)
        {
            var item = await _context.InventoryItems
                .FirstOrDefaultAsync(i => i.Id == itemId && i.UserId == userId);

            if (item == null)
                return null;

            item.Quantity = request.Quantity;
            item.MetaData = request.MetaData;
            await _context.SaveChangesAsync();

            return new InventoryItemResponse
            {
                Id = item.Id,
                ItemKey = item.ItemKey,
                Quantity = item.Quantity,
                MetaData = item.MetaData,
                ObtainedAt = item.ObtainedAt
            };
        }

        public async Task<bool> RemoveItemAsync(int userId, int itemId)
        {
            var item = await _context.InventoryItems
                .FirstOrDefaultAsync(i => i.Id == itemId && i.UserId == userId);

            if (item == null)
                return false;

            _context.InventoryItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}