# Copilot MCP Configuration

GitHub Copilot configuration files for connecting to Azure Application Insights and Azure DevOps through MCP servers.

## Quick Start

1. **Choose a configuration file:**
   - `mcp-simple.json` - Basic setup for development
   - `mcp.json` - Full configuration with documentation

2. **Configure credentials:**
   Update the `appsettings.json` files in each MCP project with your Azure credentials.

3. **Test the servers:**
   ```bash
   # Test AppInsights MCP
   cd src/MCP/AppInsightsMCP && dotnet run
   
   # Test DevOps MCP
   cd src/MCP/DevopsMCP && dotnet run
   ```

## Available Tools

### AppInsights MCP (`src/MCP/AppInsightsMCP`)
Query Azure Application Insights data with tools like `executeQuery`, `getMetric`, `getEvents`, `getExceptions`, etc.

### DevOps MCP (`src/MCP/DevopsMCP`)
Manage Azure DevOps with tools like `createWorkItems`, `getWorkItem`, `updateWorkItem`, `queryWorkItems`, etc.

## Configuration Examples

### AppInsights Settings (`src/MCP/AppInsightsMCP/appsettings.json`)
```json
{
  "AppInsights": {
    "ApplicationId": "your-application-id",
    "ApiKey": "your-api-key"
  }
}
```

### DevOps Settings (`src/MCP/DevopsMCP/appsettings.json`)
```json
{
  "AzureDevops": {
    "Organization": "your-org-name", 
    "PersonalAccessToken": "your-pat-token"
  }
}
```

## Usage Examples

**With Copilot, you can ask:**
- "Show me exceptions from the last hour"
- "Create a bug work item for the login issue"
- "Get request performance data for today"
- "Update work item 1234 to mark it as done"

## Web API Mode

Run servers as REST APIs for testing:
```bash
dotnet run --web    # AppInsights API + Swagger
dotnet run --api    # DevOps API + Swagger
```

## Prompt Templates

- `prompt-templates.md` - General developer task analysis templates
- `mcp-prompts.md` - MCP-specific prompts for Azure DevOps integration

Use these templates to quickly analyze work items, plan development, and understand task scope before starting work.
