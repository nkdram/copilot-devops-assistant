# MCP Inspector Setup Guide

Quick guide for setting up Node.js and testing MCP tools with the inspector.

## Prerequisites

- Windows 10/11
- PowerShell or Command Prompt with Administrator privileges

## Step 1: Install NVM for Windows

1. **Download NVM for Windows**
   - Go to https://github.com/coreybutler/nvm-windows/releases
   - Download the latest `nvm-setup.exe` file
   - Run the installer as Administrator

2. **Verify NVM Installation**
   ```powershell
   nvm version
   ```

## Step 2: Install Latest Node.js

1. **Install Latest LTS Version**
   ```powershell
   nvm install lts
   nvm use lts
   ```

2. **Verify Installation**
   ```powershell
   node --version
   npm --version
   ```

## Step 3: Test MCP Tools

1. **Navigate to Project Directory**
   ```powershell
   cd src\MCP\DevopsMCP
   # or
   cd src\MCP\AppInsightsMCP
   ```

2. **Run MCP Inspector**
   ```powershell
   npx @modelcontextprotocol/inspector dotnet run
   ```

3. **Access Inspector**
   - The inspector will open automatically in your default browser
   - If it doesn't open automatically, check the terminal output for the URL
   - Look for a message like: "Inspector available at http://localhost:XXXX"
   - Copy and paste that URL into your browser
   - Test available tools with sample data

## Troubleshooting

- **NVM not found**: Restart terminal as Administrator
- **Inspector won't start**: Run `dotnet build` first
- **Node version issues**: Try `nvm install 20.10.0`