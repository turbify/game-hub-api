using GameAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Każda właściwość DbSet = jedna tabela w bazie
        public DbSet<User> Users { get; set; }
        public DbSet<LeaderboardEntry> LeaderboardEntries { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<GameSave> GameSaves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USER – unikalny username i email
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Username).HasMaxLength(50).IsRequired();
                entity.Property(u => u.Email).HasMaxLength(100).IsRequired();
                entity.Property(u => u.PasswordHash).IsRequired();
            });

            // LEADERBOARD – jeden user może mieć wiele wpisów
            modelBuilder.Entity<LeaderboardEntry>(entity =>
            {
                entity.HasOne(l => l.User)
                      .WithMany(u => u.LeaderboardEntries)
                      .HasForeignKey(l => l.UserId)
                      .OnDelete(DeleteBehavior.Cascade); // usuń wpisy gdy user usunięty
            });

            // INVENTORY – jeden user może mieć wiele przedmiotów
            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.HasOne(i => i.User)
                      .WithMany(u => u.InventoryItems)
                      .HasForeignKey(i => i.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(i => i.ItemKey).HasMaxLength(100).IsRequired();
            });

            // ACHIEVEMENT – globalne definicje achievementów
            modelBuilder.Entity<Achievement>(entity =>
            {
                entity.HasIndex(a => a.Key).IsUnique();
                entity.Property(a => a.Key).HasMaxLength(100).IsRequired();
                entity.Property(a => a.Name).HasMaxLength(200).IsRequired();
            });

            // USER ACHIEVEMENT – tabela łącząca user <-> achievement
            modelBuilder.Entity<UserAchievement>(entity =>
            {
                entity.HasOne(ua => ua.User)
                      .WithMany(u => u.UserAchievements)
                      .HasForeignKey(ua => ua.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ua => ua.Achievement)
                      .WithMany(a => a.UserAchievements)
                      .HasForeignKey(ua => ua.AchievementId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Ten sam achievement nie może być odblokowany dwa razy
                entity.HasIndex(ua => new { ua.UserId, ua.AchievementId }).IsUnique();
            });

            // GAME SAVE – jeden user = jeden zapis
            modelBuilder.Entity<GameSave>(entity =>
            {
                entity.HasOne(gs => gs.User)
                      .WithOne(u => u.GameSave)
                      .HasForeignKey<GameSave>(gs => gs.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}