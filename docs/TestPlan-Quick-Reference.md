# TestPlan Service Quick Reference

## API Endpoints

### Create Test Plan with Steps & Parameters
```
POST /api/testcase
```

### Update Test Plan with Steps & Parameters
```
PUT /api/testcase/{id}
```

## Gherkin Step Types

| Type | Usage | Example |
|------|-------|---------|
| `Given` | Preconditions/Context | "Given the user is logged in" |
| `When` | Actions/Events | "When the user clicks submit" |
| `Then` | Expected Results | "Then the form should be saved" |
| `And` | Additional steps | "And a confirmation email is sent" |
| `But` | Negative conditions | "But the password should not be visible" |

## Quick Examples

### Minimal Example
```json
{
  "project": "MyProject",
  "name": "Simple Test",
  "steps": [
    {"type": "Given", "action": "user is ready", "order": 1},
    {"type": "When", "action": "action occurs", "order": 2},
    {"type": "Then", "action": "result happens", "order": 3}
  ]
}
```

### With Parameters
```json
{
  "project": "MyProject",
  "name": "Data-Driven Test",
  "steps": [...],
  "parameters": [
    {
      "name": "Test Case 1",
      "parameters": [
        {"name": "input", "value": "test"},
        {"name": "expected", "value": "success"}
      ]
    }
  ]
}
```

### Complete Example
```json
{
  "project": "MyProject",
  "name": "Full Test Plan",
  "fields": {
    "description": "Complete test",
    "priority": 1
  },
  "steps": [
    {
      "type": "Given",
      "action": "initial state",
      "order": 1,
      "data": {"key": "value"}
    },
    {
      "type": "When",
      "action": "perform action",
      "order": 2
    },
    {
      "type": "Then",
      "action": "verify result",
      "expectedResult": "Success message",
      "order": 3
    }
  ],
  "parameters": [
    {
      "name": "Scenario 1",
      "parameters": [
        {"name": "username", "value": "user1", "type": "string"},
        {"name": "count", "value": "5", "type": "int"}
      ]
    }
  ]
}
```

## Property Reference

### TestStep
```typescript
{
  type: "Given" | "When" | "Then" | "And" | "But",
  action: string,              // Required
  expectedResult?: string,     // Optional
  order: number,              // Required
  data?: object              // Optional
}
```

### TestParameter
```typescript
{
  name: string,               // Required
  value: string,              // Required
  type?: "string" | "int" | "decimal" | "boolean"  // Default: "string"
}
```

### ParameterSet
```typescript
{
  name: string,               // Optional
  parameters: TestParameter[] // Required
}
```

## Common Patterns

### Login Test
```json
{
  "steps": [
    {"type": "Given", "action": "on login page", "order": 1},
    {"type": "When", "action": "enter credentials", "order": 2},
    {"type": "And", "action": "click login", "order": 3},
    {"type": "Then", "action": "should see dashboard", "order": 4}
  ],
  "parameters": [
    {"name": "Valid", "parameters": [
      {"name": "user", "value": "testuser"},
      {"name": "pass", "value": "Test123!"}
    ]},
    {"name": "Invalid", "parameters": [
      {"name": "user", "value": "bad"},
      {"name": "pass", "value": "wrong"}
    ]}
  ]
}
```

### Form Validation
```json
{
  "steps": [
    {"type": "Given", "action": "form is displayed", "order": 1},
    {"type": "When", "action": "submit with invalid data", "order": 2},
    {"type": "Then", "action": "should show error", "expectedResult": "Validation error displayed", "order": 3}
  ],
  "parameters": [
    {"name": "Empty Field", "parameters": [
      {"name": "field", "value": "email"},
      {"name": "error", "value": "Email required"}
    ]},
    {"name": "Invalid Format", "parameters": [
      {"name": "field", "value": "email"},
      {"name": "value", "value": "notanemail"},
      {"name": "error", "value": "Invalid email"}
    ]}
  ]
}
```

### API Testing
```json
{
  "steps": [
    {"type": "Given", "action": "API is available", "order": 1},
    {"type": "When", "action": "send GET request to /users", "order": 2},
    {"type": "Then", "action": "should return 200 OK", "order": 3},
    {"type": "And", "action": "response should contain user list", "order": 4}
  ],
  "parameters": [
    {"name": "Page 1", "parameters": [
      {"name": "page", "value": "1"},
      {"name": "expectedCount", "value": "10"}
    ]},
    {"name": "Page 2", "parameters": [
      {"name": "page", "value": "2"},
      {"name": "expectedCount", "value": "10"}
    ]}
  ]
}
```

## Tips

1. **Order Steps**: Always use sequential order numbers (1, 2, 3...)
2. **Be Specific**: Write clear, actionable steps
3. **Expected Results**: Always specify for "Then" steps
4. **Parameter Names**: Use descriptive names for parameter sets
5. **Data Types**: Specify types for non-string parameters
6. **Reusability**: Keep steps generic to reuse with different parameters

## Response Format

The API returns formatted data:
```json
{
  "testSteps": [
    {
      "type": "Given",
      "action": "user is ready",
      "gherkinFormat": "Given user is ready",
      "order": 1
    }
  ],
  "parameters": {
    "parameterSets": [...],
    "tableView": {
      "headers": ["username", "password"],
      "rows": [{"username": "user1", "password": "pass1"}]
    }
  }
}
```
