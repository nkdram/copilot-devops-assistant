# DevOps MCP Server - Complete Tool Reference

## Overview
The DevOps MCP Server provides comprehensive tools for managing Azure DevOps work items and test plans, including support for Gherkin-formatted test steps and data-driven testing with parameter tables.

## Available Tools

### 📋 Work Item Tools

#### 1. `createWorkItems`
Creates a work item in Azure DevOps.

**Parameters:**
- `project` (string) - Project name or ID
- `type` (string) - Work item type (e.g., 'Bug', 'Task', 'User Story', 'Test Case')
- `fields` (object) - Dictionary of field names and values

**Example:**
```json
{
  "project": "MyProject",
  "type": "Bug",
  "fields": {
    "System.Title": "Login button not working",
    "System.Description": "User cannot login",
    "Microsoft.VSTS.Common.Priority": 1
  }
}
```

#### 2. `getWorkItem`
Gets a work item by its ID (including test cases, since they're work items).

**Parameters:**
- `id` (number) - Work item ID

**Example:**
```json
{
  "id": 1229857
}
```

**Usage to check test case:**
```
@workspace use devops-mcp getWorkItem with id 1229857
```

#### 3. `updateWorkItem`
Updates a work item by its ID.

**Parameters:**
- `id` (number) - Work item ID
- `fields` (object) - Dictionary of field names and values to update

**Example:**
```json
{
  "id": 1229857,
  "fields": {
    "System.State": "Active",
    "System.AssignedTo": "user@example.com"
  }
}
```

#### 4. `deleteWorkItem`
Deletes a work item by its ID.

**Parameters:**
- `id` (number) - Work item ID

#### 5. `getTags`
Gets tags for a work item.

**Parameters:**
- `id` (number) - Work item ID

#### 6. `addTags`
Adds tags to a work item.

**Parameters:**
- `id` (number) - Work item ID
- `tags` (array) - Array of tag strings to add

**Example:**
```json
{
  "id": 1229857,
  "tags": ["urgent", "bug-fix", "production"]
}
```

#### 7. `removeTags`
Removes tags from a work item.

**Parameters:**
- `id` (number) - Work item ID
- `tags` (array) - Array of tag strings to remove

#### 8. `getComments`
Gets comments for a work item.

**Parameters:**
- `id` (number) - Work item ID

#### 9. `addComment`
Adds a comment to a work item.

**Parameters:**
- `id` (number) - Work item ID
- `comment` (string) - Comment text to add

---

### 🧪 Test Plan Tools

#### 10. `createTestPlan`
Creates a test plan with optional Gherkin-formatted test steps and parameter tables.

**Parameters:**
- `project` (string) - Project name or ID
- `name` (string) - Test plan name
- `fields` (object) - Dictionary of field names and values
- `steps` (array, optional) - Array of TestStep objects with Gherkin format
- `parameters` (array, optional) - Array of ParameterSet objects

**TestStep Structure:**
```json
{
  "type": "Given|When|Then|And|But",
  "action": "Description of the step",
  "expectedResult": "Expected outcome (optional)",
  "order": 1,
  "data": {}
}
```

**ParameterSet Structure:**
```json
{
  "name": "Test Scenario Name",
  "parameters": [
    {
      "name": "username",
      "value": "testuser",
      "type": "string"
    }
  ]
}
```

**Example:**
```json
{
  "project": "MyProject",
  "name": "Login Test Plan",
  "fields": {
    "description": "Test login functionality",
    "state": "Active"
  },
  "steps": [
    {
      "type": "Given",
      "action": "the user is on the login page",
      "order": 1
    },
    {
      "type": "When",
      "action": "the user enters valid credentials",
      "order": 2
    },
    {
      "type": "Then",
      "action": "the user should be logged in",
      "expectedResult": "Dashboard is displayed",
      "order": 3
    }
  ],
  "parameters": [
    {
      "name": "Valid User",
      "parameters": [
        {"name": "username", "value": "testuser"},
        {"name": "password", "value": "Test123!"}
      ]
    }
  ]
}
```

#### 11. `getTestPlan`
Gets a test plan by its ID.

**Parameters:**
- `id` (number) - Test plan ID

**Example:**
```json
{
  "id": 12345
}
```

#### 12. `updateTestPlan`
Updates a test plan with optional Gherkin steps and parameters.

**Parameters:**
- `id` (number) - Test plan ID
- `fields` (object) - Dictionary of field names and values to update
- `steps` (array, optional) - Array of TestStep objects
- `parameters` (array, optional) - Array of ParameterSet objects

#### 13. `deleteTestPlan`
Deletes a test plan by its ID.

**Parameters:**
- `id` (number) - Test plan ID

#### 14. `getTestPlanTags`
Gets tags for a test plan.

**Parameters:**
- `id` (number) - Test plan ID

#### 15. `addTestPlanTags`
Adds tags to a test plan.

**Parameters:**
- `id` (number) - Test plan ID
- `tags` (array) - Array of tag strings to add

#### 16. `removeTestPlanTags`
Removes tags from a test plan.

**Parameters:**
- `id` (number) - Test plan ID
- `tags` (array) - Array of tag strings to remove

#### 17. `getTestPlanComments`
Gets comments for a test plan.

**Parameters:**
- `id` (number) - Test plan ID

#### 18. `addTestPlanComment`
Adds a comment to a test plan.

**Parameters:**
- `id` (number) - Test plan ID
- `comment` (string) - Comment text to add

---

## Usage Examples

### Check Test Case 1229857

Since test cases are work items in Azure DevOps:

**Using Copilot Chat:**
```
@workspace use devops-mcp getWorkItem with id 1229857
```

**Direct MCP Call:**
```json
{
  "tool": "getWorkItem",
  "parameters": {
    "id": 1229857
  }
}
```

### Create Test Plan with Gherkin Steps

```
@workspace use devops-mcp createTestPlan with project "MyProject", name "API Test Plan", and include Gherkin steps for testing REST endpoints
```

### Update Test Case with Tags

```
@workspace use devops-mcp addTags to work item 1229857 with tags ["automated", "regression", "critical"]
```

### Get All Test Plan Details

```
@workspace use devops-mcp getTestPlan with id 12345
```

---

## Gherkin Format Support

### Supported Keywords
- **Given**: Preconditions or initial context
- **When**: Actions or events
- **Then**: Expected outcomes or results
- **And**: Additional steps of the same type
- **But**: Negative conditions or exceptions

### Example Gherkin Test
```gherkin
Given the user is logged in
And the shopping cart contains items
When the user clicks checkout
And enters payment information
Then the order should be processed
And a confirmation email should be sent
```

---

## Data-Driven Testing

Use parameter sets to test multiple scenarios with the same test steps:

```json
{
  "steps": [...],
  "parameters": [
    {
      "name": "Valid Login",
      "parameters": [
        {"name": "username", "value": "user1"},
        {"name": "password", "value": "pass1"},
        {"name": "expected", "value": "success"}
      ]
    },
    {
      "name": "Invalid Password",
      "parameters": [
        {"name": "username", "value": "user1"},
        {"name": "password", "value": "wrong"},
        {"name": "expected", "value": "error"}
      ]
    }
  ]
}
```

---

## Configuration

### .copilot/mcp.json
```json
{
  "mcpServers": {
    "devops-mcp": {
      "command": "dotnet",
      "args": ["run", "--project", "src/MCP/DevopsMCP/DevopsMCP.csproj"],
      "env": {
        "DOTNET_ENVIRONMENT": "Development"
      }
    }
  }
}
```

### .vscode/mcp.json
```json
{
  "servers": {
    "devops": {
      "command": "dotnet",
      "args": ["run", "--project", "src/MCP/DevopsMCP"],
      "env": {
        "DOTNET_ENVIRONMENT": "Development"
      }
    }
  }
}
```

---

## Notes

1. **Test Cases as Work Items**: Azure DevOps test cases are a type of work item, so you can use `getWorkItem` to retrieve test case details.

2. **API Versions**: The service uses Azure DevOps REST API version 7.1-preview for both work items and test plans.

3. **Authentication**: Ensure your `appsettings.json` is configured with valid Azure DevOps credentials:
   ```json
   {
     "AzureDevops": {
       "Organization": "your-org",
       "Project": "your-project",
       "PersonalAccessToken": "your-pat"
     }
   }
   ```

4. **MCP vs Web API Mode**: The server can run in MCP mode (for MCP clients) or Web API mode (for HTTP access with Swagger UI).

---

### Test Suite Tools

#### 16. `getTestSuite`
Gets a test suite by plan ID and suite ID.

**Parameters:**
- `planId` (number) - Test plan ID
- `suiteId` (number) - Test suite ID

**Example:**
```json
{
  "planId": 552002,
  "suiteId": 957589
}
```

#### 17. `addTestCasesToSuite`
Adds test cases to a test suite.

**Parameters:**
- `planId` (number) - Test plan ID
- `suiteId` (number) - Test suite ID
- `testCaseIds` (array of numbers) - Array of test case IDs to add

**Example:**
```json
{
  "planId": 552002,
  "suiteId": 957589,
  "testCaseIds": [1239495, 1239496, 1239497, 1239498]
}
```

#### 18. `removeTestCasesFromSuite`
Removes test cases from a test suite.

**Parameters:**
- `planId` (number) - Test plan ID
- `suiteId` (number) - Test suite ID
- `testCaseIds` (array of numbers) - Array of test case IDs to remove

#### 19. `getTestCasesInSuite`
Gets all test cases in a test suite.

**Parameters:**
- `planId` (number) - Test plan ID
- `suiteId` (number) - Test suite ID

#### 20. `deleteTestCases`
Deletes test cases using the Test Management API. Note: Test cases must be removed from all suites before deletion.

**Parameters:**
- `testCaseIds` (number[]) - Array of test case IDs to delete

**Example:**
```json
{
  "testCaseIds": [1239495, 1239496, 1239497, 1239498]
}
```

**Usage:**
```
@workspace use devops-mcp deleteTestCases with testCaseIds [1239495, 1239496, 1239497, 1239498]
```

**Important Notes:**
- Test case work items CANNOT be deleted using the standard Work Items API
- You must use this Test Management API endpoint: `_apis/test/testcases/{id}`
- Best practice: Remove test cases from suites first using `removeTestCasesFromSuite`

---

## Quick Reference

| Tool | Purpose | Key Parameters |
|------|---------|----------------|
| `getWorkItem` | Get test case/work item | `id` |
| `createWorkItems` | Create new work item | `project`, `type`, `fields` |
| `updateWorkItem` | Update work item | `id`, `fields` |
| `getTestPlan` | Get test plan | `id` |
| `createTestPlan` | Create test plan with Gherkin | `project`, `name`, `steps`, `parameters` |
| `addTags` | Add tags | `id`, `tags` |
| `addComment` | Add comment | `id`, `comment` |
| `getTestSuite` | Get test suite | `planId`, `suiteId` |
| `addTestCasesToSuite` | Add test cases to suite | `planId`, `suiteId`, `testCaseIds` |
| `removeTestCasesFromSuite` | Remove test cases from suite | `planId`, `suiteId`, `testCaseIds` |
| `getTestCasesInSuite` | Get test cases in suite | `planId`, `suiteId` |
| `deleteTestCases` | Delete test cases | `testCaseIds` |
