# Implementation Plan: [FEATURE]

**Branch**: `[###-feature-name]` | **Date**: [DATE] | **Spec**: [link]
**Input**: Feature specification from `/specs/[###-feature-name]/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

Add document upload and management to the ContosoDashboard training app. Implementation will be integrated into the existing Blazor Server app using EF Core for metadata and a local filesystem-backed `IFileStorageService` for file storage. The training implementation will simulate virus scanning and implement soft-delete (30-day recovery) for safety. Design follows the repository constitution: offline-first, simple, and documented for production migration.

## Technical Context

<!--
  ACTION REQUIRED: Replace the content in this section with the technical details
  for the project. The structure here is presented in advisory capacity to guide
  the iteration process.
-->

**Language/Version**: C# / .NET 8.0
**Primary Dependencies**: ASP.NET Core 8.0, Blazor Server, Entity Framework Core, Bootstrap (UI), bUnit/xUnit for tests
**Storage**: SQL Server LocalDB via EF Core for metadata; local filesystem (configured `AppData/uploads`) for file storage
**Testing**: xUnit for services, bUnit for Blazor component tests, integration tests using test server where feasible
**Target Platform**: Cross-platform (Linux/Windows) development environment; Blazor Server web app
**Project Type**: Web application (Blazor Server) integrated into existing ContosoDashboard project
**Performance Goals**: Upload of files up to 25 MB within 30s; document list/search responses ≤2s for up to 500 documents
**Constraints**: Offline-first (no external cloud deps by default); files stored outside `wwwroot`; `DocumentId` must be integer; MIME type field length 255
**Scale/Scope**: Training feature sized for hundreds of documents and dozens of concurrent users in a lab environment

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

Gates evaluated against `.specify/memory/constitution.md`:

- Training-First (MANDATORY): Compliant — feature is explicitly training-oriented and documented as non-production-ready.
- Offline-First & Cloud-Ready (REQUIRED): Compliant — local filesystem storage and `IFileStorageService` abstraction provided; no cloud services by default.
- Test-First (STRONGLY RECOMMENDED): Advisory — plan includes test targets (unit + component tests) but tests are not strictly gating for initial Phase 0 research.
- Security-by-Example (REQUIRED): Compliant — security considerations (files outside `wwwroot`, authorization checks, simulated scanning documented) are included in design and will be called out in implementation notes.
- Simplicity & Observability (REQUIRED): Compliant — design favors simple, observable patterns and structured logging for examples.

No constitution violations detected that would block Phase 0 research.

## Project Structure

### Documentation (this feature)

```text
specs/[###-feature]/
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)
<!--
  ACTION REQUIRED: Replace the placeholder tree below with the concrete layout
  for this feature. Delete unused options and expand the chosen structure with
  real paths (e.g., apps/admin, packages/something). The delivered plan must
  not include Option labels.
-->

```text
# [REMOVE IF UNUSED] Option 1: Single project (DEFAULT)
src/
├── models/
├── services/
├── cli/
└── lib/

tests/
├── contract/
├── integration/
└── unit/

# [REMOVE IF UNUSED] Option 2: Web application (when "frontend" + "backend" detected)
backend/
├── src/
│   ├── models/
│   ├── services/
│   └── api/
└── tests/

frontend/
├── src/
│   ├── components/
│   ├── pages/
│   └── services/
└── tests/

# [REMOVE IF UNUSED] Option 3: Mobile + API (when "iOS/Android" detected)
api/
└── [same as backend above]

ios/ or android/
└── [platform-specific structure: feature modules, UI flows, platform tests]
```

**Structure Decision**: Integrate feature into the existing ContosoDashboard web app. Key code additions will live under:

- `Data/` — EF Core entity and migrations (Document, DocumentShare)
- `Models/` — domain models if needed
- `Services/` — `IFileStorageService`, `LocalFileStorageService`, `DocumentService`
- `Pages/Documents/` — Blazor pages/components for upload, My Documents, Project Documents, Shared With Me
- `wwwroot/` — minimal UI assets; actual files stored outside `wwwroot`

This keeps changes localized to the existing project layout and avoids introducing separate backend/frontend repos.

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| [e.g., 4th project] | [current need] | [why 3 projects insufficient] |
| [e.g., Repository pattern] | [specific problem] | [why direct DB access insufficient] |
