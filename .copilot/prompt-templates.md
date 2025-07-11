# Developer Task Analysis Prompt Template

Use this prompt template with GitHub Copilot to analyze work items before starting development tasks.

## Basic Work Item Analysis

```
Analyze work item [WORK_ITEM_ID] and provide a comprehensive summary including:

1. **Work Item Details:**
   - Title and description
   - Work item type (Bug, User Story, Task, etc.)
   - Priority and severity
   - Current state and assigned developer
   - Acceptance criteria or definition of done

2. **Context & Scope:**
   - Related work items or dependencies
   - Affected components or areas of the codebase
   - Technical requirements and constraints
   - Business impact and user value

3. **Development Planning:**
   - Estimated effort and complexity
   - Suggested approach or implementation strategy
   - Potential risks or blockers
   - Required skills or expertise

4. **Collaboration Info:**
   - Stakeholders and reviewers
   - Communication history and key decisions
   - External dependencies or approvals needed

Please format the response in a clear, actionable summary that helps me understand what needs to be done and how to approach this task.
```

## Advanced Analysis with Tags and Comments

```
Perform a detailed analysis of work item [WORK_ITEM_ID] for development planning:

**Core Information:**
- Get work item details including title, description, type, priority, and current state
- Review all tags and labels for categorization and context
- Analyze acceptance criteria and success metrics

**Historical Context:**
- Review all comments and discussion history
- Identify key decisions, changes in scope, or important clarifications
- Extract any technical specifications or design decisions from comments

**Scope Assessment:**
- Determine the full scope of work including related items
- Identify dependencies on other work items or external systems
- Assess impact on existing functionality or user experience

**Development Readiness:**
- Evaluate if the work item has sufficient detail to begin development
- Identify any missing information or unclear requirements
- Suggest questions to ask stakeholders if needed

**Action Plan:**
- Recommend a development approach and breakdown of tasks
- Identify potential challenges and mitigation strategies
- Suggest timeline and milestones for completion

Format the analysis as a development brief that I can use to start work confidently.
```

## Quick Pre-Development Checklist

```
Run a pre-development checklist for work item [WORK_ITEM_ID]:

**Requirement Clarity:** ✓/✗
- [ ] Clear and complete description
- [ ] Well-defined acceptance criteria
- [ ] Understanding of user impact
- [ ] Technical requirements specified

**Dependencies & Context:** ✓/✗
- [ ] Related work items identified
- [ ] Dependencies mapped and available
- [ ] No blocking issues
- [ ] Stakeholder alignment confirmed

**Technical Readiness:** ✓/✗
- [ ] Architecture/design approach clear
- [ ] Required tools and access available
- [ ] Test strategy defined
- [ ] Code review process understood

**Project Integration:** ✓/✗
- [ ] Fits within current sprint/milestone
- [ ] Aligns with team priorities
- [ ] Estimated effort is reasonable
- [ ] Resources are available

Provide a summary of any items that need attention before development can begin effectively.
```

## Retrospective Analysis Template

```
Analyze completed work item [WORK_ITEM_ID] for lessons learned:

**Execution Review:**
- Compare actual vs estimated effort and timeline
- Review what went well and what could be improved
- Identify any scope creep or requirement changes

**Process Insights:**
- Evaluate the effectiveness of initial planning
- Review collaboration and communication patterns
- Assess code quality and technical decisions

**Knowledge Transfer:**
- Document key technical insights or solutions
- Identify reusable patterns or components
- Note any new tools or techniques learned

**Future Improvements:**
- Suggest process improvements for similar work items
- Recommend template updates or documentation needs
- Identify training or skill development opportunities

Use this analysis to improve our development process and knowledge base.
```

## Team Planning Template

```
Prepare a team briefing for work item [WORK_ITEM_ID]:

**Executive Summary:**
- Work item purpose and business value
- High-level scope and deliverables
- Key stakeholders and timeline

**Technical Overview:**
- Components and systems involved
- Integration points and dependencies
- Quality and performance requirements

**Team Coordination:**
- Role assignments and responsibilities
- Collaboration touchpoints and reviews
- Communication plan and status updates

**Risk Management:**
- Identified risks and mitigation plans
- Escalation paths for blockers
- Contingency planning for delays

Present this in a format suitable for team standup or planning meetings.
```
