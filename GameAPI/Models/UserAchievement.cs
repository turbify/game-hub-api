namespace GameAPI.Models
{
    // table to represent the many-to-many relationship between Users and Achievements
    public class UserAchievement
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AchievementId { get; set; }
        public DateTime UnlockedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
        public Achievement Achievement { get; set; } = null!;
    }
}
