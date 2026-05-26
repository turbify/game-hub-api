namespace GameAPI.DTOs
{
    public class AddScoreRequest
    {
        public int Score { get; set; }
        public int Level { get; set; }
        public TimeSpan? PlayTime { get; set; }
    }

    public class LeaderboardEntryResponse
    {
        public int Rank { get; set; }
        public string Username { get; set; } = string.Empty;
        public int Score { get; set; }
        public int Level { get; set; }
        public TimeSpan? PlayTime { get; set; }
        public DateTime AchievedAt { get; set; }
    }
}