# Sync Impact Report
<!--
Version change: unset -> 1.0.0
Modified principles: (placeholders replaced with concrete principles)
Added sections: Core Principles, Constraints & Non-Functional Requirements, Development Workflow, Governance
Removed sections: none
Follow-up TODOs: RATIFICATION_DATE left as TODO
-->

# ContosoDashboard Constitution

## Core Principles

### 1. Training-First (MANDATORY)
The repository and its artifacts are intended for training and educational purposes only. All documentation, examples, and code MUST clearly state that this project is not production-ready. Any guidance or code used from this repository in other projects MUST be reviewed and adapted for production standards before deployment.

### 2. Offline-First & Cloud-Ready (REQUIRED)
The implementation MUST be runnable locally without external cloud dependencies and MUST provide clear abstraction boundaries (interfaces) so that cloud-backed implementations can replace local implementations without changing business logic. Infrastructure adapters (e.g., file storage, database) MUST be isolated behind interfaces.

### 3. Test-First for Learning (STRONGLY RECOMMENDED)
Tests are core learning artifacts: examples and exercises MUST include automated tests demonstrating expected behavior. Authors SHOULD write tests before implementing example features when feasible to teach test-driven development practices. Critical behaviors showcased in training modules SHOULD have passing, reproducible tests.

### 4. Security-by-Example (REQUIRED)
Security safeguards presented in this repository are educational examples. Security-relevant code and documentation MUST call out limitations and recommended production practices. Any changes that alter security-related behavior MUST include an explicit rationale and test cases demonstrating the effect.

### 5. Simplicity and Observability (REQUIRED)
The codebase MUST favor simple, readable implementations that illustrate concepts clearly. Instrumentation and structured logging SHOULD be present in examples to aid debugging and teaching; telemetry or observability integrations included for training MUST be clearly labeled as examples and opt-in.

## Constraints & Non-Functional Requirements

- Technology stack: ASP.NET Core 8.0, Blazor Server, EF Core. Tooling and examples MUST target the stated stack unless a migration path is documented.
- No external services: The training implementation MUST avoid external cloud services by default. Any example that integrates external services MUST include a local fallback and clear instructions for production configuration.
- Data handling: Seed and example data MUST not contain sensitive or real user data. Sample accounts and seed data are mocked for training only.

## Development Workflow

- Contributions: Amendments to this constitution or training content MUST be submitted as a pull request targeting `.specify/memory/constitution.md` or the relevant doc files.
- Reviews: Changes that affect learning outcomes, security guidance, or architecture MUST be reviewed by at least one project maintainer and one instructor/owner when available.
- Testing gates: PRs that modify example behavior or security-relevant code MUST include updated tests and documentation demonstrating the change.
- Release process: Documentation or sample changes intended for training releases SHOULD include a short changelog entry describing the pedagogical impact.

## Governance

Amendments to this constitution are managed via pull requests to `.specify/memory/constitution.md`. Each amendment MUST include:

- A description of the change and rationale.
- A migration or teaching note if the change affects existing exercises or examples.
- The proposed version bump according to the policy below.

Versioning policy:

- MAJOR: Backward-incompatible governance or principle redefinitions (increments when a principle is removed or materially redefined).
- MINOR: Addition of a new principle/section or material expansion of an existing principle.
- PATCH: Clarifications, wording fixes, or non-substantive refinements.

Compliance review expectations:

- Maintaining authors MUST ensure that training modules referencing this constitution remain accurate. Significant deviations discovered during course delivery MUST be corrected with an accompanying amendment PR.

**Version**: 1.0.0 | **Ratified**: TODO(RATIFICATION_DATE): original ratification date unknown | **Last Amended**: 2026-09-20
