# 🎮 GameAPI

A backend REST API for games built with ASP.NET Core 8.0. Handles player authentication, leaderboard, inventory, achievements, and a game save system. Designed for use with HTML5 (Phaser.js) and Unity games.

---

## ✨ Features

- 🔐 **Authentication** – register and login with JWT
- 🏆 **Leaderboard** – global player rankings
- 🎒 **Inventory** – player item management
- 🥇 **Achievements** – achievement system with unlock tracking
- 💾 **Save System** – save and load game state
- 👑 **Roles** – role-based access control (Admin / Player)

---

## 🛠️ Tech Stack

| Technology | Version |
|---|---|
| ASP.NET Core | 8.0 |
| Entity Framework Core | 9.0 |
| SQL Server | 2022 |
| Docker | latest |
| JWT Bearer | 8.0 |
| FluentValidation | latest |
| Serilog | latest |
| Swagger UI | 6.9 |

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### 1. Clone the repository

```bash
git clone https://github.com/turbify/game-hub-api
cd GameAPI
```

### 2. Start the database

```bash
docker-compose up -d
```

### 3. Configure secrets

Right-click on the **GameAPI** project → **Manage User Secrets** and fill:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=GameBackendDB;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True"
  },
  "JwtSettings": {
    "SecretKey": "YOUR-SECRET-KEY-MINIMUM-32-CHARACTERS"
  }
}
```

Or via terminal:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=GameBackendDB;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True"
dotnet user-secrets set "JwtSettings:SecretKey" "YOUR-SECRET-KEY-MINIMUM-32-CHARACTERS"
```

### 4. Apply migrations

```bash
dotnet ef database update
```

Or in Visual Studio Package Manager Console:

```powershell
Update-Database
```

### 5. Run the project

```bash
dotnet run --project GameAPI
```

Swagger UI available at: `https://localhost:7185/swagger`

---

## 📡 Endpoints

### Auth
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| POST | `/api/Auth/register` | Register a new player | ❌ |
| POST | `/api/Auth/login` | Login and receive JWT | ❌ |

### Leaderboard
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Leaderboard/top` | Get top scores | ❌ |
| GET | `/api/Leaderboard/my-scores` | Get my scores | ✅ |
| POST | `/api/Leaderboard` | Submit a score | ✅ |

### Inventory
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Inventory` | Get my inventory | ✅ |
| POST | `/api/Inventory` | Add an item | ✅ |
| PUT | `/api/Inventory/{id}` | Update an item | ✅ |
| DELETE | `/api/Inventory/{id}` | Remove an item | ✅ |

### Achievements
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Achievement` | Get all achievements | ❌ |
| GET | `/api/Achievement/my` | Get my achievements | ✅ |
| POST | `/api/Achievement` | Create an achievement | 👑 Admin |
| POST | `/api/Achievement/unlock/{key}` | Unlock an achievement | ✅ |

### Save System
| Method | Endpoint | Description | Auth |
|---|---|---|---|
| GET | `/api/Save` | Load game save | ✅ |
| POST | `/api/Save` | Save game state | ✅ |
| DELETE | `/api/Save` | Delete game save | ✅ |

---

## 🔐 Authentication

This API uses **JWT Bearer Token** authentication. After logging in, include the token in every protected request:

```
Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
```

In Swagger UI click the **Authorize 🔒** button and enter `Bearer {token}`.

---

## 📁 Project Structure

```
GameAPI/
├── Controllers/        # API endpoints
├── Data/               # DbContext (Entity Framework)
├── DTOs/               # Request and response objects
├── Extensions/         # Service configuration
├── Middleware/         # Global Exception Handler
├── Models/             # Database models
├── Services/           # Business logic
├── Validators/         # Request validation (FluentValidation)
├── logs/               # Application logs (Serilog)
├── appsettings.json
├── appsettings.Example.json
└── Program.cs
```

---

## 🗄️ Database Schema

```
Users
├── Id, Username, Email, PasswordHash, Role
├── → LeaderboardEntries (1:N)
├── → InventoryItems (1:N)
├── → UserAchievements (1:N)
└── → GameSave (1:1)

Achievements
└── → UserAchievements (1:N)
```

---

## 📋 Logging

Logs are stored in the `logs/` folder with daily rotation (last 7 days retained):

---

## 🔒 Rate Limiting

| Endpoint | Limit |
|---|---|
| POST `/api/Auth/login` | 5 requests / minute |
| POST `/api/Auth/register` | 3 requests / minute |
| POST `/api/Save` | 30 requests / minute |
| All other endpoints | 100 requests / minute |

---

## 🎮 Game Client Compatibility

This API is designed for integration with:
- **HTML5 / Phaser.js** – via `fetch()` or `axios`
- **Unity** – via `UnityWebRequest`

---

## 📄 License

MIT