# Getting Started with the AE Toolkit

This guide helps solution and delivery leads understand what the AE toolkit is, which challenges it addresses, and when to deploy each module. It expands upon the basic information in [`ae-toolkit/README.md`](../README.md) with contextual guidance for different project situations.

## Table of Contents

- [Who This Is For](#who-this-is-for)
- [Keys to Success](#keys-to-success)
- [Toolkit Focus Areas](#toolkit-focus-areas)
  - [Establish AI Development Practices](#establish-ai-development-practices)
  - [Assessing AI Development Effectiveness](#assessing-ai-development-effectiveness)
  - [Documentation Refreshing](#documentation-refreshing)

## Who This Is For

- **Solution/delivery leads** who are technically capable software developers
- Teams that may or may not have extensive experience deploying AI development tools
- Anyone responsible for setting AI patterns and tooling for their team
- Teams looking to move beyond ad-hoc AI tool usage to systematic AI-accelerated engineering

Since each project is different, this guide provides flexible guidance that adapts to various contexts rather than one-size-fits-all solutions.

## Quick Start vs. Systematic Approach

### Quick Start (Recommended for Most Teams)

**Best for:** Teams wanting immediate productivity gains with minimal setup complexity.

**Approach:** Start with ready-to-use examples from [`../examples/`](../examples/), customize them for your context, and deploy. This gets you productive in 15 minutes.

**When to use:**
- Small to medium teams (2-8 developers)
- Projects with straightforward AI development needs
- Teams new to AI development practices
- Situations where you want to iterate and refine based on actual usage

See the [Quick Start guide in ae-toolkit/README.md](../README.md#quick-start-recommended-for-most-teams) for step-by-step instructions.

### Systematic Approach (For Complex Scenarios)

**Best for:** Teams needing comprehensive analysis, greenfield projects, or complex retrofits.

**Approach:** Use advanced toolkit modules for structured assessment, analysis-driven customization, and sophisticated coordination.

**When to use:**
- Large teams (8+ developers) needing coordinated rollout
- Greenfield projects building AI practices from day one
- Complex existing projects with unique requirements
- Teams experiencing significant AI interaction inefficiencies
- Projects with rapidly evolving documentation needs

Continue reading this guide to understand which advanced modules apply to your specific scenario.


## Keys to Success

**You *MUST* understand these foundational concepts before deploying any toolkit modules.**

#### **Review, validate, and customize all toolkit-generated artifacts** 
*Thoroughly examine any documentation, rules, or configurations created by AE modules to ensure they accurately reflect your project's architecture, patterns, and team practices before adopting them*

#### **Maintain AI documentation as living processes, not static artifacts**
*Treat AI context documentation and behavioral rules as evolving technical debt that requires active maintenance. Integrate updates into your Definition of Done, establish team expectations for collaborative improvement, and create feedback loops to capture what's working and what needs refinement as your project evolves*


## Toolkit Focus Areas

### Establish AI Development Practices

Your team needs systematic AI development practices instead of ad-hoc individual usage. This applies whether you're starting a new project from scratch or retrofitting an existing project where developers are using AI tools sporadically with no coordination.

**Default approach: Start with Examples**

Most teams should begin with the [Examples Collection](../examples/README.md):
1. Copy relevant rules, agents/chat modes, and commands from [`../examples/`](../examples/)
2. Customize for your coding standards, architecture patterns, and team practices
3. Deploy according to your AI tool's documentation
4. Iterate based on team feedback and actual usage

This gets you productive immediately without complex setup processes.

**When to use advanced modules instead:**

- **[AI Initializer](../ai-initializer/README.md)**: For greenfield projects needing comprehensive AI setup from day one, or complex retrofits requiring structured assessment of existing architecture. Generates customized AI context documentation and tool configurations **for the codebase you'll be actively developing with AI assistance**.

- **[Rules Manager](../rules-manager/README.md)**: For large teams (8+ developers) needing sophisticated rule coordination across multiple AI tools with intelligent context detection and modular deployment.

- **[Context Refresher](../context-refresher/README.md)**: Establish maintenance processes once initial setup is complete (whether from examples or AI Initializer).

**Common implementation paths:**
- *Small team (2-4 developers)*: Examples + generic rules + 2-3 key agents/modes
- *Medium team (5-8 developers)*: Examples + tool-specific rules + full agent/mode suite
- *Large team (8+ developers)*: AI Initializer or examples + Rules Manager + systematic rollout
- *Greenfield project with complex requirements*: AI Initializer for comprehensive assessment-driven setup

---

### Assessing AI Development Effectiveness

Your team is using AI tools but you want to understand how effective those interactions are, or you suspect there might be room for improvement in how your team works with AI tools.

**Relevant modules:**
- **[Interaction Analyzer](../interaction-analyzer/README.md)**: Diagnostic tool that analyzes AI-human interactions to identify why they're inefficient and what can be improved
- **[Context Refresher](../context-refresher/README.md)**: May reveal that poor interactions are due to outdated context documentation

**Common scenarios:**
- *Repetitive explanations*: Developers spend significant time explaining the same project concepts to AI tools in every conversation
- *Inconsistent quality*: AI suggestions vary wildly in quality, sometimes excellent, sometimes completely off-base
- *Context overload*: Conversations become too long because AI tools don't have the right background information upfront

---

### Documentation Refreshing 

Your project's AI context becomes outdated as the codebase evolves, leading to AI tools making suggestions based on stale architectural decisions, deprecated patterns, or removed/updated dependencies.

**Relevant modules:**
- **[Context Refresher](../context-refresher/README.md)**: Analyzes git history to identify what has changed since documentation was last updated, providing specific guidance on what needs updating
- **[Interaction Analyzer](../interaction-analyzer/README.md)**: Can diagnose when documentation staleness is causing inefficient interactions

**Common scenarios:**
- *Framework migration*: Project upgraded from React 16 to 18, but AI context still references old patterns and hooks
- *Architecture refactor*: Moved from monolith to microservices but documentation still describes monolithic deployment
- *API changes*: Internal APIs evolved but AI tools still generate code for deprecated endpoints
- *Team knowledge drift*: Original context authors left, current team unsure what documentation is still accurate

---

After completing this guide, your team will have deployed appropriate AE toolkit modules and established sustainable practices to maintain and scale AI adoption.

For detailed implementation guidance on any module, see the individual module READMEs linked above. For terminology and concepts, reference the [GLOSSARY.md](../GLOSSARY.md).

Remember: Start with the appropriate module based on your situation, adapt as you go, and customize for your specific context. The toolkit is designed to be foundational, not prescriptive.