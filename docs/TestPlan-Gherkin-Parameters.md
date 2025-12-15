# Test Plan with Gherkin Steps and Parameters

This document demonstrates how to use the TestPlan Service with Gherkin-formatted test steps and parameter tables for data-driven testing.

## Overview

The TestPlan Service now supports:
- **Gherkin Format Test Steps**: Given/When/Then/And/But style test steps
- **Parameter Tables**: Data-driven testing with parameter sets

## Models

### TestStep
Represents a single test step in Gherkin format.

```json
{
  "type": "Given|When|Then|And|But",
  "action": "Description of the action",
  "expectedResult": "Expected outcome (optional)",
  "order": 1,
  "data": {
    "key": "additional data"
  }
}
```

### TestParameter
Represents a single parameter.

```json
{
  "name": "username",
  "value": "testuser",
  "type": "string"
}
```

### ParameterSet
Represents a row in the parameter table (combination of parameters).

```json
{
  "name": "Test Set 1",
  "parameters": [
    { "name": "username", "value": "user1", "type": "string" },
    { "name": "password", "value": "pass1", "type": "string" }
  ]
}
```

## Example Usage

### Create Test Plan with Gherkin Steps

```json
POST /api/testcase
{
  "project": "MyProject",
  "name": "Login Test Plan",
  "fields": {
    "description": "Test plan for login functionality",
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
      "type": "And",
      "action": "clicks the login button",
      "order": 3
    },
    {
      "type": "Then",
      "action": "the user should be redirected to the dashboard",
      "expectedResult": "Dashboard page is displayed",
      "order": 4
    }
  ],
  "parameters": [
    {
      "name": "Valid User Set",
      "parameters": [
        { "name": "username", "value": "testuser1", "type": "string" },
        { "name": "password", "value": "Pass123!", "type": "string" },
        { "name": "expectedPage", "value": "Dashboard", "type": "string" }
      ]
    },
    {
      "name": "Admin User Set",
      "parameters": [
        { "name": "username", "value": "admin", "type": "string" },
        { "name": "password", "value": "Admin123!", "type": "string" },
        { "name": "expectedPage", "value": "Admin Dashboard", "type": "string" }
      ]
    }
  ]
}
```

### Update Test Plan with New Steps

```json
PUT /api/testcase/{id}
{
  "fields": {
    "description": "Updated test plan with error handling"
  },
  "steps": [
    {
      "type": "Given",
      "action": "the user is on the login page",
      "order": 1
    },
    {
      "type": "When",
      "action": "the user enters invalid credentials",
      "order": 2
    },
    {
      "type": "Then",
      "action": "an error message should be displayed",
      "expectedResult": "Error: Invalid username or password",
      "order": 3
    }
  ],
  "parameters": [
    {
      "name": "Invalid Password",
      "parameters": [
        { "name": "username", "value": "testuser1", "type": "string" },
        { "name": "password", "value": "wrong", "type": "string" }
      ]
    }
  ]
}
```

## Gherkin Keywords

The service supports the following Gherkin keywords:
- **Given**: Initial context or preconditions
- **When**: Action or event
- **Then**: Expected outcome
- **And**: Additional steps of the same type
- **But**: Negative or exception conditions

## Data-Driven Testing with Parameters

Parameter tables allow you to run the same test with different input data. Each `ParameterSet` represents one iteration of the test with specific values.

### Parameter Table Example

| username  | password  | expectedResult |
|-----------|-----------|----------------|
| user1     | Pass123!  | Success        |
| user2     | Pass456!  | Success        |
| admin     | Admin123! | Success        |

This translates to:

```json
{
  "parameters": [
    {
      "name": "Set 1",
      "parameters": [
        { "name": "username", "value": "user1" },
        { "name": "password", "value": "Pass123!" },
        { "name": "expectedResult", "value": "Success" }
      ]
    },
    {
      "name": "Set 2",
      "parameters": [
        { "name": "username", "value": "user2" },
        { "name": "password", "value": "Pass456!" },
        { "name": "expectedResult", "value": "Success" }
      ]
    },
    {
      "name": "Set 3",
      "parameters": [
        { "name": "username", "value": "admin" },
        { "name": "password", "value": "Admin123!" },
        { "name": "expectedResult", "value": "Success" }
      ]
    }
  ]
}
```

## Response Format

The service formats the test steps and parameters as follows:

### Test Steps Response
```json
{
  "testSteps": [
    {
      "type": "Given",
      "action": "the user is on the login page",
      "expectedResult": "",
      "order": 1,
      "gherkinFormat": "Given the user is on the login page",
      "data": null
    }
  ]
}
```

### Parameters Response
```json
{
  "parameters": {
    "parameterSets": [...],
    "tableView": {
      "headers": ["username", "password", "expectedResult"],
      "rows": [
        { "username": "user1", "password": "Pass123!", "expectedResult": "Success" }
      ]
    }
  }
}
```

## Best Practices

1. **Order Steps Logically**: Use the `order` property to maintain test step sequence
2. **Be Specific**: Write clear, unambiguous actions and expected results
3. **Use Parameters Wisely**: Group related test data in parameter sets
4. **Name Parameter Sets**: Give meaningful names to parameter sets for clarity
5. **Expected Results**: Always specify expected results for validation steps
