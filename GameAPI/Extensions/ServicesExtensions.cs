using FluentValidation;
using FluentValidation.AspNetCore;
using GameAPI.Services;

namespace GameAPI.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddGameServices(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<LeaderboardService>();
            services.AddScoped<InventoryService>();
            services.AddScoped<AchievementService>();
            services.AddScoped<SaveService>();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<Program>();

            return services;
        }
    }
}