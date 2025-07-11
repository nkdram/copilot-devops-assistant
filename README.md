# Copilot DevOps Assistant

A powerful Model Context Protocol (MCP) server collection for working with Azure DevOps and Azure Application Insights to streamline bug fixing and system monitoring.

## 🚀 Features

This project provides two specialized MCP servers:

### 📋 DevopsMCP - Azure DevOps Integration
- **Work Item Management**: Create, read, update, and delete work items
- **Tag Management**: Add, remove, and query tags on work items
- **Comment System**: Add and retrieve comments on work items
- **Full CRUD Operations**: Complete work item lifecycle management

### 📊 AppInsightsMCP - Azure Application Insights Integration
- **Custom KQL Queries**: Execute Kusto Query Language queries
- **Application Metrics**: Get application performance metrics
- **Exception Tracking**: Query and analyze application exceptions
- **Request Analytics**: Monitor HTTP requests and dependencies
- **Trace Analysis**: Access application logs and traces
- **Performance Monitoring**: Get performance counters and diagnostics

## 🏗️ Architecture

```
copilot-devops-assistant/
├── src/MCP/
│   ├── DevopsMCP/           # Azure DevOps MCP Server
│   │   ├── Service/         # Business logic layer
│   │   ├── Tools/           # MCP tool definitions
│   │   ├── Settings/        # Configuration models
│   │   └── Program.cs       # Application entry point
│   └── AppInsightsMCP/      # Application Insights MCP Server
│       ├── Service/         # Business logic layer
│       ├── Tools/           # MCP tool definitions
│       ├── Settings/        # Configuration models
│       └── Program.cs       # Application entry point
└── docs/
    └── MCP-Inspector.md     # Quick setup guide
```

## ⚙️ Configuration

### DevopsMCP Configuration

Update `src/MCP/DevopsMCP/appsettings.json`:

```json
{
  "AzureDevops": {
    "Organization": "your-organization-name",
    "Project": "your-project-name", 
    "PersonalAccessToken": "your-pat-token"
  }
}
```

**Required Permissions for PAT Token:**
- Work Items: Read & Write
- Work Items: Read, Write & Manage (for tags)

### AppInsightsMCP Configuration

Update `src/MCP/AppInsightsMCP/appsettings.json`:

```json
{
  "AppInsights": {
    "ApplicationId": "your-application-id",
    "ApiKey": "your-api-key",
    "WorkspaceId": "your-workspace-id"
  }
}
```

**How to Get Credentials:**
1. **Application ID**: Found in Application Insights → Overview
2. **API Key**: Application Insights → API Access → Create API key
3. **Workspace ID**: Log Analytics workspace → Properties

## 🔧 Available Tools

### DevopsMCP Tools

| Tool | Description | Parameters |
|------|-------------|------------|
| `createWorkItems` | Creates a new work item | project, type, fields |
| `getWorkItem` | Retrieves work item by ID | id |
| `updateWorkItem` | Updates existing work item | id, fields |
| `deleteWorkItem` | Deletes a work item | id |
| `getTags` | Gets all tags for a work item | id |
| `addTags` | Adds tags to a work item | id, tags |
| `removeTags` | Removes tags from a work item | id, tags |
| `getComments` | Gets all comments for a work item | id |
| `addComment` | Adds a comment to a work item | id, comment |

### AppInsightsMCP Tools

| Tool | Description | Parameters |
|------|-------------|------------|
| `executeQuery` | Execute custom KQL query | query, timespan |
| `getApplicationInfo` | Get application information | - |
| `getMetric` | Get specific metric data | metricName, timespan, aggregation |
| `getEvents` | Get events by type | eventType, timespan, top |
| `getExceptions` | Get application exceptions | timespan, top |
| `getRequests` | Get HTTP request data | timespan, top |
| `getDependencies` | Get dependency call data | timespan, top |
| `getTraces` | Get application traces/logs | timespan, top |
| `getPerformanceCounters` | Get performance metrics | timespan, top |

## 🚦 Quick Start

1. **Setup Environment**: Follow [MCP Inspector Guide](docs/MCP-Inspector.md)
2. **Configure Settings**: Update appsettings.json files with your credentials
3. **Test Tools**: Use MCP Inspector to test functionality
4. **Integrate**: Connect with your AI assistant

## 🧪 Testing with MCP Inspector

For detailed testing instructions, see [MCP Inspector Setup Guide](docs/MCP-Inspector.md).

**Quick Test:**
```powershell
# Navigate to desired project
cd src\MCP\DevopsMCP

# Run with inspector
npx @modelcontextprotocol/inspector dotnet run
```

## 📝 Usage Examples

### DevopsMCP Examples

**Create a Bug Work Item:**
```json
{
  "project": "MyProject",
  "type": "Bug",
  "fields": {
    "System.Title": "Login page not responding",
    "System.Description": "Users unable to access login page",
    "Microsoft.VSTS.Common.Priority": 1,
    "Microsoft.VSTS.Common.Severity": "2 - High"
  }
}
```

**Add Tags to Work Item:**
```json
{
  "id": 12345,
  "tags": ["urgent", "frontend", "login"]
}
```

### AppInsightsMCP Examples

**Query Recent Exceptions:**
```json
{
  "query": "exceptions | where timestamp > ago(1h) | summarize count() by problemId | order by count_ desc",
  "timespan": "PT1H"
}
```

**Get Application Performance:**
```json
{
  "metricName": "requests/duration",
  "timespan": "PT24H",
  "aggregation": "avg"
}
```

## 🔍 Troubleshooting

### Common Issues

**Authentication Errors:**
- Verify PAT token permissions for DevOps
- Check API key validity for Application Insights
- Ensure organization/project names are correct

**Connection Issues:**
- Verify network connectivity to Azure services
- Check if corporate firewall blocks API calls
- Validate base URLs and endpoints

**Build Errors:**
- Run `dotnet restore` in project directory
- Ensure .NET 9.0 SDK is installed
- Check for missing NuGet packages


