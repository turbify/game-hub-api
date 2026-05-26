namespace GameAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Role { get; set; } = "Player"; // default role, can be "Player", "Admin", etc.
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; } = true;

        // navigate to related data (EF Core will populate it automatically)
        public ICollection<LeaderboardEntry> LeaderboardEntries { get; set; } = new List<LeaderboardEntry>();
        public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
        public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
        public GameSave? GameSave { get; set; }
    }
}