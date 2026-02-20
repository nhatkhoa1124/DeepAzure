# DeepAzure – Monster Battle Game Server

A production-ready REST API backend for a turn-based monster battle game, built with ASP.NET Core 9, featuring JWT authentication, layered architecture with Repository Pattern, and containerized deployment with Docker.

## Features

- **Layered Architecture** – Clean separation with Controller → Service → Repository pattern
- **JWT Authentication** – Secure token-based auth with ASP.NET Core Identity
- **Generic Repository Pattern** – Reusable data access with LINQ expression trees for dynamic includes
- **Paginated API Responses** – Consistent pagination abstraction across all list endpoints
- **Soft Delete Support** – Global query filters for audit-safe data management
- **Element Type System** – Configurable damage multipliers via ElementMatchup relationships
- **Dockerized Environment** – Docker Compose orchestration with PostgreSQL and pgAdmin
- **Test-Driven Development** – Unit tests with xUnit, Moq, and FluentAssertions
- **Global Exception Handling** – Centralized error handling with ProblemDetails responses

## Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                     Presentation Layer                          │
│              (Controllers + Global Exception Handler)           │
├─────────────────────────────────────────────────────────────────┤
│                     Application Layer                           │
│                (Services + Extension Mappers)                   │
├─────────────────────────────────────────────────────────────────┤
│                    Infrastructure Layer                         │
│          (Generic Repository + EF Core Configurations)          │
├─────────────────────────────────────────────────────────────────┤
│                      Database Layer                             │
│                (PostgreSQL + Identity Tables)                   │
└─────────────────────────────────────────────────────────────────┘
```

Dependency Injection ensures loose coupling between layers. Each layer communicates only through interfaces, enabling isolated unit testing and future scalability.

## Technical Highlights

### Generic Repository with Expression Trees
Implemented a reusable `IRepository<T>` interface with dynamic eager loading using `Expression<Func<T, object>>[]` parameters, enabling flexible query composition without code duplication.

```csharp
Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
Task<PagedResult<T>> GetPagedAsync(int pageNumber, int pageSize, params Expression<Func<T, object>>[] includes);
```

### Pagination Abstraction
Designed a generic `PagedResult<T>` with a `Map<R>()` method for projecting entities to DTOs while preserving pagination metadata.

### Soft Delete with Global Query Filters
All entities inherit from `BaseAuditable` (CreatedAt, UpdatedAt, DeletedAt). EF Core global filters automatically exclude soft-deleted records from all queries.

### JWT Authentication Pipeline
Custom JWT Bearer configuration with:
- Zero clock skew for precise token expiration
- Custom `OnChallenge` handler returning structured JSON errors
- Role-based authorization with Identity integration

### Entity-to-DTO Mapping via Extension Methods
Clean mapping layer using static extension methods (e.g., `monster.ToResponseDto()`), avoiding heavy AutoMapper overhead.

### Docker Multi-Stage Build with Test Gate
Dockerfile includes a test stage that fails the build if unit tests don't pass, enforcing quality before deployment.

## Domain Model

### Core Game Entities

| Entity | Description |
|--------|-------------|
| `Monster` | Base creature template with stats, growth rates, elements, and abilities |
| `Element` | Type system (Fire, Water, Electric, etc.) for damage calculations |
| `ElementMatchup` | Damage multiplier matrix defining type advantages/weaknesses |
| `Technique` | Battle moves with physical/magic damage, targeting, and status effects |
| `Ability` | Passive effects with JSON-based logic configuration for extensibility |
| `StatusEffect` | Conditions applied during battle (Poison, Paralyze, Burn, etc.) |
| `Item` | Consumables with effect types and power values |
| `Badge` | Achievement/progression markers for players |

### User-Owned Entities

| Entity | Description |
|--------|-------------|
| `User` | Player account extending ASP.NET Identity with ELO rating and avatar |
| `UserMonster` | Player-owned monster instance with level, EXP, nickname, and 4 technique slots |
| `UserItem` | Player inventory tracking item quantities |
| `UserBadge` | Unlocked achievements with timestamp |

### Multiplayer & Social

| Entity | Description |
|--------|-------------|
| `Match` | PvP battle record with status, type, and replay log |
| `UserMatch` | Join table tracking team snapshots, ELO changes, and outcomes |
| `Conversation` | Private chat thread between two users |
| `Message` | Individual message within a conversation |


## Tech Stack

| Category | Technology |
|----------|------------|
| Framework | ASP.NET Core 9.0 |
| Language | C# 13 |
| Database | PostgreSQL 16 |
| ORM | Entity Framework Core 9 |
| Authentication | ASP.NET Core Identity + JWT Bearer |
| Testing | xUnit, Moq, FluentAssertions |
| Containerization | Docker, Docker Compose |
| API Documentation | Scalar (OpenAPI) |

## Project Structure

```
DeepAzure/
├── DeepAzureServer/                           
│   ├── Controllers/                           
│   ├── Services/                              
│   │   ├── Interfaces/                        # Service contracts
│   │   └── Implementations/                   # Service implementations
│   ├── Repositories/                          
│   │   ├── Interfaces/                        # Repository contracts
│   │   └── Implementations/                   # Repository implementations with EF Core
│   ├── Models/                                
│   │   ├── Entities/                          # EF Core entity classes (database tables)
│   │   ├── Requests/                          # API request DTOs
│   │   ├── Responses/                         # API response DTOs
│   │   ├── Enums/                             # Game enumerations (status effects, item types, etc.)
│   │   └── Common/                            # Shared models (PagedResult, etc.)
│   ├── Data/                                  
│   │   └── Configurations/                    # EF Core Fluent API configurations
│   ├── Utils/                                 
│   │   └── Extensions/                        # Entity-to-DTO mapping extensions
│   ├── Infrastructures/                       # Cross-cutting concerns (exception handling, etc.)
│   └── Migrations/                            # EF Core database migrations
├── DeepAzureServer.Tests/                     
│   ├── Services/                              # Service layer tests
│   └── Helpers/                               # Test utilities & mocks
├── DeepAzureServer.IntegrationTests/          
├── docker-compose.yml                         
└── README.md                                  
```

## Running the Project

### Prerequisites
- Docker & Docker Compose
- .NET 9 SDK (for local development)

### With Docker (Recommended)

```bash
# Clone repository
git clone https://github.com/nhatkhoa1124/DeepAzure.git
cd DeepAzure

# Create .env file
cp .env.example .env

# Start services
docker-compose up --build

# Access API at http://localhost:5000
# Access pgAdmin at http://localhost:5050
```

### Local Development

```bash
# Start PostgreSQL only
docker-compose up postgres -d

# Apply migrations
dotnet ef database update --project DeepAzureServer

# Run API
dotnet run --project DeepAzureServer

# Run tests
dotnet test
```

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/register` | Register new user |
| POST | `/api/auth/login` | Authenticate user |
| GET | `/api/monster?pageNumber=1&pageSize=20` | Get paginated monsters |
| GET | `/api/monster/{id}` | Get monster by ID |
| GET | `/api/element` | Get all elements |
| GET | `/api/ability?pageNumber=1&pageSize=20` | Get paginated abilities |

## Key Technical Decisions

| Decision | Rationale |
|----------|-----------|
| **Generic Repository over direct DbContext** | Enables unit testing with mocks, consistent query patterns |
| **Extension methods for mapping** | Lightweight alternative to AutoMapper, explicit control |
| **Global query filters for soft delete** | Prevents accidental exposure of deleted data |
| **JWT over session-based auth** | Stateless API, horizontal scaling ready |
| **PostgreSQL over SQL Server** | Open-source, JSON support, containerization simplicity |
| **Separate test project** | Isolated test dependencies, cleaner CI/CD pipeline |

## Testing

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test class
dotnet test --filter "FullyQualifiedName~AuthServiceTests"
```

## License

MIT
