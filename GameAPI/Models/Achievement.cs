namespace GameAPI.Models
{
    // Definicja achievementu (globalna, dla wszystkich graczy)
    public class Achievement
    {
        public int Id { get; set; }
        public string Key { get; set; } = string.Empty;         // np. "first_kill"
        public string Name { get; set; } = string.Empty;        // np. "Pierwszy Cios"
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
        public int Points { get; set; } = 0;

        public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
    }
}
