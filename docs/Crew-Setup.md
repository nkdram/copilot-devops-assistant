# Crew AI Setup Guide

This guide will help you set up Crew AI for building multi-agent AI systems that can work with Azure DevOps and Application Insights.

## Prerequisites

- Windows 10/11 with PowerShell 5.1 or later
- Administrator privileges for installation
- Internet connection for downloading tools

## Step 1: Install Python Version Manager (pyenv)

Install pyenv-win to manage Python versions:

```powershell
# Download and install pyenv-win
Invoke-WebRequest -UseBasicParsing -Uri "https://raw.githubusercontent.com/pyenv-win/pyenv-win/master/pyenv-win/install-pyenv-win.ps1" -OutFile "./install-pyenv-win.ps1"
&"./install-pyenv-win.ps1"
```

**After installation:**
1. Restart your PowerShell session
2. Verify installation: `pyenv --version`

## Step 2: Install Python 3.12

Install the recommended Python version for Crew AI:

```powershell
# List available Python versions
pyenv install --list

# Install Python 3.12 (latest stable)
pyenv install 3.12

# Set as global default
pyenv global 3.12

# Verify installation
python --version
```

## Step 3: Install UV Package Manager

UV is a fast Python package manager that works well with Crew AI:

```powershell
# Install UV package manager
powershell -ExecutionPolicy ByPass -c "irm https://astral.sh/uv/install.ps1 | iex"
```

**After installation:**
1. Restart your PowerShell session
2. Verify installation: `uv --version`

## Step 4: Install Crew AI

Install Crew AI as a global tool:

```powershell
# Install Crew AI CLI
uv tool install crewai

# Verify installation
crewai --version
```

## Next Steps

1. **Create Agents**: Define agents for different DevOps tasks
2. **Connect to MCP**: Integrate with your MCP servers
3. **Build Workflows**: Create automated DevOps workflows
4. **Test Integration**: Verify Azure DevOps and App Insights connectivity

## Troubleshooting

### Common Issues

**pyenv not recognized:**
- Restart PowerShell session
- Check PATH environment variable
- Reinstall with administrator privileges

**UV installation fails:**
- Check PowerShell execution policy: `Get-ExecutionPolicy`
- Set policy if needed: `Set-ExecutionPolicy RemoteSigned -Scope CurrentUser`

**Crew AI import errors:**
- Ensure virtual environment is activated
- Reinstall dependencies: `uv sync`
- Check Python version compatibility

### Useful Commands

```powershell
# List pyenv versions
pyenv versions

# List UV tools
uv tool list

# Update Crew AI
uv tool upgrade crewai

# Activate virtual environment
.venv\Scripts\Activate.ps1

# Deactivate virtual environment
deactivate
```

## Resources

- [Crew AI Documentation](https://docs.crewai.com/)
- [UV Package Manager](https://docs.astral.sh/uv/)
- [pyenv-win GitHub](https://github.com/pyenv-win/pyenv-win)
- [Azure DevOps Python API](https://github.com/Microsoft/azure-devops-python-api)