# Docker Compose Guide

## Prerequisites
- Docker Desktop installed
- Docker Compose v3.8 or higher

## Setup

1. **Copy the environment template:**
   ```bash
   cp .env.example .env
   ```

2. **Configure your environment variables in `.env`:**
   ```bash
   # Azure DevOps Configuration
   AZDO_ORGANIZATION=MarsDevTeam
   AZDO_PROJECT=RoyalCaninSitecore
   AZDO_PAT=your-personal-access-token

   # Application Insights Configuration
   APPINSIGHTS_APP_ID=your-app-insights-application-id
   APPINSIGHTS_API_KEY=your-app-insights-api-key
   ```

## Usage

### Build and Start All Services
```bash
docker-compose up -d --build
```

### Start Specific Service
```bash
# Start only DevOpsMCP
docker-compose up -d devopsmcp

# Start only AppInsightsMCP
docker-compose up -d appinsightsmcp
```

### View Logs
```bash
# All services
docker-compose logs -f

# Specific service
docker-compose logs -f devopsmcp
docker-compose logs -f appinsightsmcp
```

### Stop Services
```bash
# Stop all
docker-compose down

# Stop specific service
docker-compose stop devopsmcp
docker-compose stop appinsightsmcp
```

### Rebuild After Code Changes
```bash
# Rebuild all
docker-compose build

# Rebuild specific service
docker-compose build devopsmcp
docker-compose build appinsightsmcp

# Rebuild and restart
docker-compose up -d --build
```

## Service Endpoints

### DevOpsMCP
- **MCP Mode (stdio)**: Default mode, communicates via stdin/stdout
- **Web API Mode**: Add `--web` flag
  - Base URL: http://localhost:5001
  - Swagger UI: http://localhost:5001/swagger

### AppInsightsMCP
- **MCP Mode (stdio)**: Default mode, communicates via stdin/stdout
- **Web API Mode**: Add `--web` flag
  - Base URL: http://localhost:5002
  - Swagger UI: http://localhost:5002/swagger

## Running in Web API Mode

To run services in Web API mode with Swagger UI, modify the `docker-compose.yml`:

```yaml
services:
  devopsmcp:
    # ... other config
    command: ["--web"]  # Add this line
    
  appinsightsmcp:
    # ... other config
    command: ["--web"]  # Add this line
```

Then restart:
```bash
docker-compose up -d
```

## Container Management

### Access Container Shell
```bash
# DevOpsMCP
docker exec -it devopsmcp /bin/bash

# AppInsightsMCP
docker exec -it appinsightsmcp /bin/bash
```

### Check Container Status
```bash
docker-compose ps
```

### View Resource Usage
```bash
docker stats devopsmcp appinsightsmcp
```

### Remove Containers and Volumes
```bash
docker-compose down -v
```

## Troubleshooting

### Check if services are running
```bash
docker-compose ps
```

### View recent logs
```bash
docker-compose logs --tail=50
```

### Restart a service
```bash
docker-compose restart devopsmcp
```

### Check container health
```bash
docker inspect devopsmcp --format='{{.State.Status}}'
```

### Clean rebuild (removes cache)
```bash
docker-compose down
docker-compose build --no-cache
docker-compose up -d
```

## Network

Both services are connected to the `mcp-network` bridge network and can communicate with each other using service names:
- `devopsmcp` - accessible at `http://devopsmcp:8080` within the network
- `appinsightsmcp` - accessible at `http://appinsightsmcp:8080` within the network

## Volumes

Configuration files are mounted as read-only volumes:
- `./src/MCP/DevopsMCP/appsettings.Development.json` → `/app/appsettings.Development.json`
- `./src/MCP/AppInsightsMCP/appsettings.Development.json` → `/app/appsettings.Development.json`

## Development Workflow

1. Make code changes in `./src/MCP/DevopsMCP` or `./src/MCP/AppInsightsMCP`
2. Rebuild the affected service:
   ```bash
   docker-compose build devopsmcp
   ```
3. Restart the service:
   ```bash
   docker-compose up -d devopsmcp
   ```
4. Check logs for errors:
   ```bash
   docker-compose logs -f devopsmcp
   ```

## Production Deployment

For production, use the published images from GitHub Container Registry:

```yaml
services:
  devopsmcp:
    image: ghcr.io/nkdram/copilot-devops-assistant/devopsmcp:main
    # ... rest of config
    
  appinsightsmcp:
    image: ghcr.io/nkdram/copilot-devops-assistant/appinsightsmcp:main
    # ... rest of config
```

## Security Notes

- Never commit `.env` file with actual credentials
- Use secrets management in production (Azure Key Vault, Docker Secrets, etc.)
- Personal Access Tokens should have minimum required permissions
- Rotate tokens regularly
