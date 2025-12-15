# Real-World Example: E-Commerce Checkout Test

This example demonstrates a complete test plan for an e-commerce checkout process with Gherkin steps and parameter-driven testing.

## Test Scenario: Checkout Process

### API Request

```json
POST /api/testcase
{
  "project": "ECommerceProject",
  "name": "Checkout Flow - Multiple Payment Methods",
  "fields": {
    "description": "Comprehensive test for checkout process with various payment methods and user scenarios",
    "state": "Active",
    "priority": 1,
    "area": "Checkout",
    "iteration": "Sprint 10"
  },
  "steps": [
    {
      "type": "Given",
      "action": "the user has items in their shopping cart",
      "order": 1,
      "data": {
        "minimumItems": 1
      }
    },
    {
      "type": "And",
      "action": "the user is logged in",
      "order": 2
    },
    {
      "type": "When",
      "action": "the user navigates to the checkout page",
      "order": 3
    },
    {
      "type": "And",
      "action": "enters shipping information",
      "order": 4,
      "data": {
        "fields": ["name", "address", "city", "zip"]
      }
    },
    {
      "type": "And",
      "action": "selects a payment method",
      "order": 5
    },
    {
      "type": "And",
      "action": "enters payment details",
      "order": 6
    },
    {
      "type": "And",
      "action": "clicks the 'Complete Purchase' button",
      "order": 7
    },
    {
      "type": "Then",
      "action": "the order should be processed successfully",
      "expectedResult": "Order confirmation page is displayed with order number",
      "order": 8
    },
    {
      "type": "And",
      "action": "a confirmation email should be sent",
      "expectedResult": "Email received within 5 minutes",
      "order": 9
    },
    {
      "type": "And",
      "action": "the order should appear in order history",
      "expectedResult": "Order visible in user's account with 'Processing' status",
      "order": 10
    }
  ],
  "parameters": [
    {
      "name": "Credit Card - Standard User",
      "parameters": [
        { "name": "userType", "value": "Standard", "type": "string" },
        { "name": "paymentMethod", "value": "Credit Card", "type": "string" },
        { "name": "cardNumber", "value": "4111111111111111", "type": "string" },
        { "name": "cartTotal", "value": "99.99", "type": "decimal" },
        { "name": "shippingMethod", "value": "Standard", "type": "string" },
        { "name": "expectedDeliveryDays", "value": "5-7", "type": "string" }
      ]
    },
    {
      "name": "PayPal - Premium User",
      "parameters": [
        { "name": "userType", "value": "Premium", "type": "string" },
        { "name": "paymentMethod", "value": "PayPal", "type": "string" },
        { "name": "paypalEmail", "value": "test@example.com", "type": "string" },
        { "name": "cartTotal", "value": "249.99", "type": "decimal" },
        { "name": "shippingMethod", "value": "Express", "type": "string" },
        { "name": "expectedDeliveryDays", "value": "2-3", "type": "string" }
      ]
    },
    {
      "name": "Debit Card - Guest User",
      "parameters": [
        { "name": "userType", "value": "Guest", "type": "string" },
        { "name": "paymentMethod", "value": "Debit Card", "type": "string" },
        { "name": "cardNumber", "value": "5555555555554444", "type": "string" },
        { "name": "cartTotal", "value": "49.99", "type": "decimal" },
        { "name": "shippingMethod", "value": "Standard", "type": "string" },
        { "name": "expectedDeliveryDays", "value": "5-7", "type": "string" }
      ]
    },
    {
      "name": "Gift Card + Credit Card - Standard User",
      "parameters": [
        { "name": "userType", "value": "Standard", "type": "string" },
        { "name": "paymentMethod", "value": "Gift Card + Credit Card", "type": "string" },
        { "name": "giftCardAmount", "value": "50.00", "type": "decimal" },
        { "name": "cardNumber", "value": "4111111111111111", "type": "string" },
        { "name": "cartTotal", "value": "150.00", "type": "decimal" },
        { "name": "shippingMethod", "value": "Standard", "type": "string" },
        { "name": "expectedDeliveryDays", "value": "5-7", "type": "string" }
      ]
    }
  ]
}
```

## Negative Test Scenario: Invalid Payment

```json
POST /api/testcase
{
  "project": "ECommerceProject",
  "name": "Checkout - Payment Validation",
  "fields": {
    "description": "Test payment validation with invalid card details",
    "state": "Active",
    "priority": 2
  },
  "steps": [
    {
      "type": "Given",
      "action": "the user is on the checkout page",
      "order": 1
    },
    {
      "type": "And",
      "action": "all required fields are filled",
      "order": 2
    },
    {
      "type": "When",
      "action": "the user enters invalid payment details",
      "order": 3
    },
    {
      "type": "And",
      "action": "clicks 'Complete Purchase'",
      "order": 4
    },
    {
      "type": "Then",
      "action": "an error message should be displayed",
      "expectedResult": "Error: Invalid payment information",
      "order": 5
    },
    {
      "type": "And",
      "action": "the user should remain on the checkout page",
      "expectedResult": "URL does not change",
      "order": 6
    },
    {
      "type": "And",
      "action": "no order should be created",
      "expectedResult": "Order count in database unchanged",
      "order": 7
    }
  ],
  "parameters": [
    {
      "name": "Invalid Card Number",
      "parameters": [
        { "name": "cardNumber", "value": "1234567890123456", "type": "string" },
        { "name": "errorMessage", "value": "Invalid card number", "type": "string" }
      ]
    },
    {
      "name": "Expired Card",
      "parameters": [
        { "name": "cardNumber", "value": "4111111111111111", "type": "string" },
        { "name": "expiryDate", "value": "01/2020", "type": "string" },
        { "name": "errorMessage", "value": "Card has expired", "type": "string" }
      ]
    },
    {
      "name": "Insufficient Funds",
      "parameters": [
        { "name": "cardNumber", "value": "4000000000000002", "type": "string" },
        { "name": "errorMessage", "value": "Insufficient funds", "type": "string" }
      ]
    },
    {
      "name": "Invalid CVV",
      "parameters": [
        { "name": "cardNumber", "value": "4111111111111111", "type": "string" },
        { "name": "cvv", "value": "00", "type": "string" },
        { "name": "errorMessage", "value": "Invalid security code", "type": "string" }
      ]
    }
  ]
}
```

## Boundary Testing Example

```json
POST /api/testcase
{
  "project": "ECommerceProject",
  "name": "Checkout - Cart Value Boundaries",
  "fields": {
    "description": "Test checkout behavior at cart value boundaries (min order, free shipping threshold, etc.)",
    "state": "Active"
  },
  "steps": [
    {
      "type": "Given",
      "action": "the user has items in cart with specific total value",
      "order": 1
    },
    {
      "type": "When",
      "action": "the user proceeds to checkout",
      "order": 2
    },
    {
      "type": "Then",
      "action": "the correct shipping charges should be applied",
      "expectedResult": "Shipping cost matches business rules",
      "order": 3
    }
  ],
  "parameters": [
    {
      "name": "Below Minimum Order ($10)",
      "parameters": [
        { "name": "cartValue", "value": "9.99", "type": "decimal" },
        { "name": "expectedShipping", "value": "0.00", "type": "decimal" },
        { "name": "canCheckout", "value": "false", "type": "boolean" },
        { "name": "errorMessage", "value": "Minimum order value is $10", "type": "string" }
      ]
    },
    {
      "name": "Minimum Order ($10)",
      "parameters": [
        { "name": "cartValue", "value": "10.00", "type": "decimal" },
        { "name": "expectedShipping", "value": "5.99", "type": "decimal" },
        { "name": "canCheckout", "value": "true", "type": "boolean" }
      ]
    },
    {
      "name": "Just Below Free Shipping ($49.99)",
      "parameters": [
        { "name": "cartValue", "value": "49.99", "type": "decimal" },
        { "name": "expectedShipping", "value": "5.99", "type": "decimal" },
        { "name": "canCheckout", "value": "true", "type": "boolean" }
      ]
    },
    {
      "name": "Free Shipping Threshold ($50)",
      "parameters": [
        { "name": "cartValue", "value": "50.00", "type": "decimal" },
        { "name": "expectedShipping", "value": "0.00", "type": "decimal" },
        { "name": "canCheckout", "value": "true", "type": "boolean" }
      ]
    },
    {
      "name": "Maximum Cart Value ($10,000)",
      "parameters": [
        { "name": "cartValue", "value": "10000.00", "type": "decimal" },
        { "name": "expectedShipping", "value": "0.00", "type": "decimal" },
        { "name": "canCheckout", "value": "true", "type": "boolean" },
        { "name": "requiresVerification", "value": "true", "type": "boolean" }
      ]
    }
  ]
}
```

## Benefits of This Approach

### 1. **Clear Test Structure**
The Gherkin format makes tests readable for both technical and non-technical stakeholders.

### 2. **Data-Driven Testing**
Multiple test scenarios can be covered with the same test steps but different parameter values.

### 3. **Maintainability**
When business rules change, you can update parameters without modifying test steps.

### 4. **Comprehensive Coverage**
The parameter table format makes it easy to see all test variations at a glance.

### 5. **Automated Execution Ready**
The structured format is ideal for automation frameworks that support Gherkin and data tables.
