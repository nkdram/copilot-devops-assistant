# MCP-Powered Developer Prompts

These prompt templates leverage the Azure DevOps MCP server to gather work item information efficiently.

## Work Item Deep Dive

```
Using the DevOps MCP server, help me understand work item [WORK_ITEM_ID]:

1. First, get the full work item details
2. Retrieve all tags associated with this work item
3. If this is part of a larger feature, query for related work items
4. Analyze the work item type and suggest the appropriate development approach

Based on this information, provide:
- **Task Summary**: What exactly needs to be built/fixed
- **Scope Analysis**: How big is this task and what's involved
- **Technical Approach**: Recommended implementation strategy
- **Dependencies**: What other work or systems this depends on
- **Definition of Done**: Clear completion criteria

Please format this as a development brief I can reference throughout the task.
```

## Sprint Planning Assistant

```
Help me plan my development work using the Azure DevOps MCP:

1. Query all work items assigned to me in the current sprint
2. Get the tags and priority for each item
3. Identify any dependencies between my work items
4. Check for any blocking or high-priority items

Then provide:
- **Work Queue**: Prioritized list of my tasks
- **Time Estimates**: Rough effort estimates for planning
- **Risk Assessment**: Items that might cause delays
- **Collaboration Needs**: Tasks requiring coordination with others
- **Daily Focus**: Suggested order of work to maximize efficiency

Format this as a actionable sprint plan.
```

## Code Review Preparation

```
Before I start development on work item [WORK_ITEM_ID], help me prepare:

1. Get the complete work item details and acceptance criteria
2. Review any related work items or parent features
3. Check the work item history for any important decisions or changes
4. Identify the work item type to understand review requirements

Provide:
- **Acceptance Criteria Checklist**: What must be implemented
- **Review Focus Areas**: What reviewers should pay attention to
- **Testing Strategy**: How to validate the implementation
- **Documentation Needs**: What documentation should be updated
- **Stakeholder Communication**: Who needs to be informed of progress

This will help ensure my development meets all requirements and expectations.
```

## Bug Investigation Starter

```
I need to investigate and fix work item [WORK_ITEM_ID]. Help me get started:

1. Get the full bug report details including steps to reproduce
2. Check for any related bug reports or work items
3. Review tags to understand the component/area affected
4. Look for any previous attempts to fix this issue

Analyze and provide:
- **Problem Statement**: Clear description of what's broken
- **Impact Assessment**: Who and what is affected
- **Investigation Plan**: Step-by-step debugging approach
- **Root Cause Hypothesis**: Likely causes based on the description
- **Fix Strategy**: Recommended approach to resolve the issue
- **Testing Plan**: How to verify the fix works

Format this as a bug investigation and resolution guide.
```

## Feature Development Kickoff

```
I'm starting development on user story [WORK_ITEM_ID]. Set me up for success:

1. Get the full user story with acceptance criteria
2. Query for any child tasks or related work items
3. Check tags for feature area and technical requirements
4. Look for any linked design documents or specifications

Create a development plan including:
- **User Value**: What this feature delivers to users
- **Technical Requirements**: What needs to be built
- **Implementation Phases**: How to break down the work
- **Integration Points**: Where this connects with existing systems
- **Quality Gates**: Testing and review checkpoints
- **Success Metrics**: How we'll know it's working well

Present this as a feature development roadmap.
```

## Work Item Health Check

```
Evaluate if work item [WORK_ITEM_ID] is ready for development:

1. Get all work item details and current state
2. Check if acceptance criteria are clearly defined
3. Review tags for proper categorization and priority
4. Verify all required information is present

Provide a readiness assessment:
- **✅ Ready to Start**: Clear requirements and no blockers
- **⚠️ Needs Clarification**: Missing information or unclear requirements
- **🚫 Blocked**: Dependencies or issues preventing progress

For any issues found, suggest:
- **Questions to Ask**: What needs clarification from stakeholders
- **Missing Information**: What details are needed
- **Recommended Actions**: Steps to make the work item development-ready

This helps ensure I don't start work on poorly defined tasks.
```

## Usage Instructions

1. Replace `[WORK_ITEM_ID]` with the actual work item number
2. Copy the appropriate prompt template
3. Paste it into your chat with GitHub Copilot
4. The MCP server will automatically fetch the required Azure DevOps data
5. Review the analysis and use it to guide your development work

## Tips for Best Results

- **Be Specific**: Include exact work item IDs rather than general descriptions
- **Context Matters**: Mention your role (developer, tester, PM) for tailored advice
- **Follow Up**: Ask clarifying questions based on the initial analysis
- **Iterate**: Use different templates as you progress through the development lifecycle
