# Setup Guide

This guide will help you set up the DeepAzure project locally or with Docker.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for containerized setup)
- [PostgreSQL 16](https://www.postgresql.org/download/) (for local development without Docker)

## Initial Setup

### 1. Clone the Repository

```bash
git clone https://github.com/nhatkhoa1124/DeepAzure.git
cd DeepAzure
```

### 2. Configure Environment Variables

#### For Docker (Recommended)

```bash
# Copy the example file
cp .env.example .env

# Edit .env and replace placeholders:
# - DB_PASSWORD
# - PGADMIN_PASSWORD
# - JWT_SECRET_KEY
```

#### For Local Development

```bash
# Copy the example file
cp appsettings.json.example DeepAzureServer/appsettings.json

# Edit appsettings.json and replace:
# - DefaultConnection password
# - JwtSettings.SecretKey
```

### 3. Generate Secure Keys

Generate a secure JWT secret key (minimum 32 characters):

```bash
# On Linux/Mac:
openssl rand -base64 32

# On Windows (PowerShell):
[Convert]::ToBase64String((1..32 | ForEach-Object { Get-Random -Minimum 0 -Maximum 256 }))
```

## Running the Project

### Option A: With Docker (Recommended)

```bash
# Start all services (API + PostgreSQL + pgAdmin)
docker-compose up --build

# The API will be available at:
# - http://localhost:5000
# - https://localhost:5001

# pgAdmin will be available at:
# - http://localhost:5050
```

**Database Connection in pgAdmin:**
- Host: `postgres`
- Port: `5432`
- Database: `DeepAzure`
- Username: (from `.env` file)
- Password: (from `.env` file)

### Option B: Local Development

```bash
# 1. Start PostgreSQL (if using Docker for DB only)
docker-compose up postgres -d

# 2. Apply database migrations
dotnet ef database update --project DeepAzureServer

# 3. Run the API
dotnet run --project DeepAzureServer

# 4. Run tests
dotnet test
```

## Verifying the Setup

### 1. Check API Health

```bash
curl http://localhost:5000/health
```

### 2. Test Registration

```bash
curl -X POST http://localhost:5000/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "username": "testuser",
    "email": "test@example.com",
    "password": "Test123!"
  }'
```

### 3. Test Login

```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Test123!"
  }'
```

## Common Issues

### Issue: "Npgsql.PostgresException: 42P01: relation does not exist"

**Solution:** Run migrations

```bash
dotnet ef database update --project DeepAzureServer
```

### Issue: "Connection refused" to PostgreSQL

**Solution:** Check if PostgreSQL is running

```bash
# For Docker:
docker ps | grep postgres

# For local PostgreSQL:
# On Linux/Mac:
pg_isready

# On Windows:
# Check Services for "PostgreSQL" service
```

### Issue: JWT authentication fails

**Solution:** Ensure your JWT secret key is at least 32 characters and matches across all configuration files.

## Database Migrations

```bash
# Create a new migration
dotnet ef migrations add MigrationName --project DeepAzureServer

# Apply migrations
dotnet ef database update --project DeepAzureServer

# Rollback to a specific migration
dotnet ef database update PreviousMigrationName --project DeepAzureServer

# Remove last migration (if not applied to DB)
dotnet ef migrations remove --project DeepAzureServer
```

## Troubleshooting

### Reset Database

```bash
# With Docker:
docker-compose down -v
docker-compose up --build

# With local PostgreSQL:
dotnet ef database drop --project DeepAzureServer
dotnet ef database update --project DeepAzureServer
```

### View Logs

```bash
# Docker logs:
docker-compose logs api
docker-compose logs postgres

# Live logs:
docker-compose logs -f
```

## Security Reminders

- ✅ **Never commit** `.env` or `appsettings.json` with real credentials
- ✅ **Always use** `.env.example` and `appsettings.json.example` templates
- ✅ **Generate unique** JWT secret keys for each environment
- ✅ **Use strong passwords** for database and pgAdmin
- ✅ **Rotate secrets** regularly in production

## Next Steps

1. Explore the API documentation at `http://localhost:5000/scalar/v1`
2. Check the [README.md](./README.md) for architecture details
3. Review the test suite in `DeepAzureServer.Tests/`
4. Read the [Contributing Guide](./CONTRIBUTING.md) if you want to contribute
