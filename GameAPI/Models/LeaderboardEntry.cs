namespace GameAPI.Models
{
    public class LeaderboardEntry
    {
        public int Id { get; set; }
        public int UserId { get; set; }        // klucz obcy do User
        public int Score { get; set; }
        public int Level { get; set; }
        public TimeSpan? PlayTime { get; set; }
        public DateTime AchievedAt { get; set; } = DateTime.UtcNow;

        // Nawigacja
        public User User { get; set; } = null!;
    }
}