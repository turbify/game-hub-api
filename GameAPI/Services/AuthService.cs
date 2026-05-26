using GameAPI.Data;
using GameAPI.DTOs;
using GameAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameAPI.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
        {
            // Sprawdź czy username lub email już istnieje
            bool userExists = await _context.Users.AnyAsync(u =>
                u.Username == request.Username ||
                u.Email == request.Email);

            if (userExists)
                return null;

            // Stwórz nowego usera z zahaszowanym hasłem
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Zwróć token od razu po rejestracji
            return GenerateToken(user);
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            // Znajdź usera po username
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Username == request.Username);

            // Sprawdź czy user istnieje i hasło się zgadza
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            // Zaktualizuj datę ostatniego logowania
            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return GenerateToken(user);
        }

        private AuthResponse GenerateToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"]!;
            var expirationHours = int.Parse(jwtSettings["ExpirationHours"]!);
            var expiresAt = DateTime.UtcNow.AddHours(expirationHours);

            // Claims – dane zakodowane w tokenie (widoczne po zdekodowaniu)
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials
            );

            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = user.Username,
                ExpiresAt = expiresAt
            };
        }
    }
}