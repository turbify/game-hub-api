namespace GameAPI.Models
{
    public class LeaderboardEntry
    {
        public int Id { get; set; }
        public int UserId { get; set; }        // key to associate with the User entity
        public int Score { get; set; }
        public int Level { get; set; }
        public TimeSpan? PlayTime { get; set; }
        public DateTime AchievedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public User User { get; set; } = null!;
    }
}