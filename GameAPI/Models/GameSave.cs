namespace GameAPI.Models
{
    public class GameSave
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SaveData { get; set; } = string.Empty;  // JSON with game-specific data
        public int CurrentLevel { get; set; } = 1;
        public int CurrentScore { get; set; } = 0;
        public TimeSpan TotalPlayTime { get; set; } = TimeSpan.Zero;
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
    }
}