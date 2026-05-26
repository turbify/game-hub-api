namespace GameAPI.Models
{
    // definition of the Achievement entity, representing an achievement that can be earned by users
    public class Achievement
    {
        public int Id { get; set; }
        public string Key { get; set; } = string.Empty; 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
        public int Points { get; set; } = 0;

        public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
    }
}
