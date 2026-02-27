# AE Toolkit

A collection of tools for practicing AI-accelerated engineering in your projects. Each tool addresses specific aspects of setting up, maintaining, and optimizing AI development practices on your team.

### What this is
A foundational set of tools to help you get started with AI development, designed for easy adaptation to your specific project needs.

### What this is not
A one-size-fits-all solution or a tool you'll use unchanged forever. Expect to customize and extend it as your project evolves.

## Quick Start (Recommended for Most Teams)

Get productive with AI development in 15 minutes by starting with ready-to-use templates and examples.

### 1. Browse and Copy Examples (5-10 minutes)

Explore [`./examples/`](./examples/) and copy relevant components to your project:

**Rules for Consistent AI Behavior**:
- Generic rules: [`./examples/rules/generic/`](./examples/rules/generic/)
- Tool-specific rules:
  - Copilot: [`./examples/rules/copilot/`](./examples/rules/copilot/)
  - Cursor: [`./examples/rules/cursor/`](./examples/rules/cursor/)

**Specialized AI Agents/Modes**:
- Code quality: [`./examples/agents/code-quality/`](./examples/agents/code-quality/) or [`./examples/chat-modes/`](./examples/chat-modes/)
- Testing: [`./examples/agents/testing/`](./examples/agents/testing/) or [`./examples/chat-modes/`](./examples/chat-modes/)
- Documentation: [`./examples/agents/documentation/`](./examples/agents/documentation/) or [`./examples/chat-modes/`](./examples/chat-modes/)
- Architecture: [`./examples/agents/architecture/`](./examples/agents/architecture/) or [`./examples/chat-modes/`](./examples/chat-modes/)

**Custom Commands**:
- Commands: [`./examples/commands/`](./examples/commands/) (organized by AI tool)

### 2. Customize and Deploy (5-10 minutes)

**Critical**: Templates and examples are starting points, not final solutions. You must:

1. **Adapt rules** to match your coding standards, architecture patterns, and team practices
2. **Customize agents/modes** for your specific domain knowledge and common tasks
3. **Modify commands** to fit your workflow and project structure
4. **Test with your team** before full deployment

Deploy customized components according to your AI tool's documentation.

### Common Implementation Paths

- **Small team (2-4 developers)**: Generic rules + 2-3 key agents/modes + essential commands
- **Medium team (5-8 developers)**: Tool-specific rules + full agent/mode suite + comprehensive commands
- **Large team (8+ developers)**: Full rules library + systematic rollout process + ongoing optimization

### When to Use Advanced Modules Instead

For complex scenarios or systematic analysis needs, consider the advanced modules below:
- **Greenfield projects** needing comprehensive AI setup: Use [AI Initializer](#ai-initializer)
- **Inefficient AI interactions**: Use [Interaction Analyzer](#interaction-analyzer) to diagnose issues
- **Outdated documentation**: Use [Context Refresher](#context-refresher) for maintenance
- **Sophisticated rule coordination**: Use [Rules Manager](#rules-manager) for modular deployment

## How to use

**New to AE?** Start with the Quick Start above for immediate results, or see the [Getting Started Guide](./getting-started/README.md) for guidance on when to use advanced modules.

1. **Choose the right approach** - Quick Start with examples or advanced modules for systematic analysis
2. **Follow each tool's README** for specific usage instructions
3. **Adapt and extend** the outputs as needed for your workflow

> Start with examples, adapt as you go, and customize for your specific context.

## Available Tools

### [Examples Collection](./examples/README.md) ⭐
**Start here for most projects.** Ready-to-use rules, agents, chat modes, and commands for all major AI development tools. Copy, customize, and deploy to establish AI development practices quickly.

### [AI Initializer](./ai-initializer/README.md)
For greenfield projects or comprehensive retrofits: Transform your project with structured assessment and customized AI context generation. Best for teams needing systematic analysis before establishing AI practices.

### [Context Refresher](./context-refresher/README.md)
For ongoing maintenance: Keep your AI context documentation current by analyzing git history to identify what has changed since your documentation was last updated.

### [Rules Manager](./rules-manager/README.md)
For sophisticated deployments: Deploy modular, router-based rules across different AI development tools with intelligent context detection and tool-specific adaptations.

### [Scratch Management Utilities](./scratch-management-utilities/README.md)
For complex workflows: Maintain context across multiple AI chat sessions with structured memory management and seamless handoffs.

### [Interaction Analyzer](./interaction-analyzer/README.md)
For optimization: Diagnose why AI interactions are inefficient by analyzing AI-human interactions in the context of your project's documentation infrastructure.

## Prerequisites

**Required:** Any AI coding assistant (Claude, Copilot, Cursor, etc.)

## Toolkit Structure

```
ae-toolkit/
├── ai-initializer/              # Transform existing projects for AI development
├── context-refresher/           # Keep AI context documentation current
├── examples/                    # Example methodologies, projects, and best practice libraries
│   ├── agents/                  # Pre-built agent definitions (Claude Code/OpenCode)
│   ├── chat-modes/              # Custom chat modes (Copilot/Cursor)
│   ├── commands/                # Reusable command templates (all tools)
│   ├── methodology-templates/   # File-based methodology templates
│   ├── projects/                # Example projects demonstrating AI concepts
│   └── rules/                   # Curated rules library (all tools)
├── getting-started/             # Guide for choosing the right toolkit modules
├── interaction-analyzer/        # Diagnose and optimize AI-human interactions
├── rules-manager/               # Deploy modular rules across AI development tools
├── scratch-management-utilities/# Maintain context across multiple AI chat sessions
├── GLOSSARY.md                  # Key terminology
└── README.md                    # This file
```

For additional resources and terminology, see the [GLOSSARY.md](./GLOSSARY.md).
