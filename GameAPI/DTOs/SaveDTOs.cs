namespace GameAPI.DTOs
{
    public class SaveGameRequest
    {
        public string SaveData { get; set; } = string.Empty;
        public int CurrentLevel { get; set; } = 1;
        public int CurrentScore { get; set; } = 0;
        public TimeSpan TotalPlayTime { get; set; } = TimeSpan.Zero;
    }

    public class SaveGameResponse
    {
        public int Id { get; set; }
        public string SaveData { get; set; } = string.Empty;
        public int CurrentLevel { get; set; }
        public int CurrentScore { get; set; }
        public TimeSpan TotalPlayTime { get; set; }
        public DateTime SavedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}