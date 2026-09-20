# Tasks: Document Upload and Management

**Input**: spec.md, plan.md, research.md, data-model.md, contracts/

## Phase 1: Setup (Shared Infrastructure)

- [ ] T001 [P] Add `Document` and `DocumentShare` entities in `Data/` and update `ApplicationDbContext.cs` to include `DbSet<Document>` and `DbSet<DocumentShare>` (`Data/Document.cs`, `Data/DocumentShare.cs`, `Data/ApplicationDbContext.cs`)
- [ ] T002 [P] Add EF Core migration scaffold for documents and apply to LocalDB (`dotnet ef migrations add AddDocuments && dotnet ef database update`) (repo root)
- [ ] T003 [P] Add configuration option for uploads path in `appsettings.json` and read in `Program.cs` (`appsettings.json`, `Program.cs`)
- [ ] T004 [P] Create `IFileStorageService` interface and `LocalFileStorageService` implementation (`Services/IFileStorageService.cs`, `Services/LocalFileStorageService.cs`)
- [ ] T005 [P] Add `DocumentService` business layer coordinating validation, storage, and metadata (`Services/DocumentService.cs`)
- [ ] T006 [P] Add logging and structured telemetry scaffolding for document operations (`Services/DocumentService.cs`, `Program.cs`)
- [ ] T007 [P] Add unit test project entries and basic test scaffolding (xUnit) for new services (`tests/DocumentService.Tests/DocumentServiceTests.cs`)

## Phase 2: Foundational (Blocking Prerequisites)

- [ ] T008 Setup DB migrations pipeline and verify LocalDB connection (`Data/` + `appsettings.json`)
- [ ] T009 [P] Implement authorization helper and policies for document access (project membership, roles) (`Services/AuthorizationHelpers.cs`, `Program.cs`)
- [ ] T010 [P] Add file validation utilities (size/check extension whitelist) and MIME type normalization (`Services/FileValidation.cs`)
- [ ] T011 [P] Add secure download endpoint scaffolding that serves files from storage after authorization (`Pages/Api/DocumentsController.cs` or `Controllers/DocumentsController.cs`)
- [ ] T012 [P] Add simulated scan metadata support and defaults in `DocumentService` (`Services/DocumentService.cs`, `Data/Document.cs`)

---

## Phase 3: User Story 1 - Upload Documents (Priority: P1) 🎯 MVP

**Goal**: User can upload files with metadata and see them in "My Documents" with preview/download support.

**Independent Test**: Upload a valid PDF and verify metadata persisted, file saved to disk, and preview works.

- [ ] T013 [P] [US1] Implement upload UI component and page `Pages/Documents/Upload.razor` and `Pages/Documents/MyDocuments.razor`
- [ ] T014 [US1] Implement server-side upload handling: API endpoint `POST /api/documents/upload` that accepts multipart/form-data and calls `DocumentService.UploadAsync` (`Controllers/DocumentsController.cs` or `Pages/Api/DocumentsHandler.razor.cs`)
- [ ] T015 [US1] Implement `DocumentService.UploadAsync` to: validate file, generate GUID path, save via `LocalFileStorageService`, persist `Document` record with `Scanned=true` and `ScanResult="Simulated"` (`Services/DocumentService.cs`, `Services/LocalFileStorageService.cs`, `Data/Document.cs`)
- [ ] T016 [US1] Add client-side upload progress indicator and validation in `Pages/Documents/Upload.razor` (uses InputFile/MemoryStream pattern) (`Pages/Documents/Upload.razor`)
- [ ] T017 [US1] Add unit tests for upload behavior (validation, metadata persistence) (`tests/DocumentService.Tests/UploadTests.cs`)
- [ ] T018 [US1] Add acceptance test: end-to-end upload → metadata → download preview (integration test using TestServer) (`tests/integration/DocumentUploadTests.cs`)

---

## Phase 4: User Story 2 - Browse & Search Documents (Priority: P1)

**Goal**: Users can browse their documents, sort/filter, and search by title/tags/uploader/project.

**Independent Test**: Seed documents and verify list loads and search returns correct results within performance bounds.

- [ ] T019 [P] [US2] Implement `DocumentService.ListByUserAsync` and `DocumentService.SearchAsync` (`Services/DocumentService.cs`)
- [ ] T020 [US2] Implement `Pages/Documents/MyDocuments.razor` with sorting/filtering controls and server-side paging (`Pages/Documents/MyDocuments.razor`)
- [ ] T021 [US2] Implement search endpoint `GET /api/documents/search?q=...` with authorization filtering (`Controllers/DocumentsController.cs`)
- [ ] T022 [US2] Add tests for search and list performance (unit + load test guidance) (`tests/DocumentSearch.Tests/DocumentSearchTests.cs`)
- [ ] T023 [US2] Add `Recent Documents` dashboard widget to `Shared/RecentDocuments.razor` and include in `Pages/Index.razor` (`Shared/RecentDocuments.razor`, `Pages/Index.razor`)

---

## Phase 5: User Story 3 - Share and Notifications (Priority: P2)

**Goal**: Owners can share documents with users/teams and recipients see shared documents and notifications.

**Independent Test**: Owner shares a doc; recipient sees it in "Shared with Me" and receives an in-app notification.

- [ ] T024 [P] [US3] Create `DocumentShare` model and persistence logic (`Data/DocumentShare.cs`, `Data/ApplicationDbContext.cs`)
- [ ] T025 [US3] Implement share UI in `Pages/Documents/Share.razor` and integrate with `DocumentService.ShareAsync` (`Pages/Documents/Share.razor`, `Services/DocumentService.cs`)
- [ ] T026 [US3] Implement `Shared with Me` view `Pages/Documents/SharedWithMe.razor` (`Pages/Documents/SharedWithMe.razor`)
- [ ] T027 [US3] Integrate with `NotificationService` to emit in-app notifications when a document is shared (`Services/NotificationService.cs`, `Services/DocumentService.cs`)
- [ ] T028 [US3] Add tests for sharing and notification flows (`tests/DocumentShare.Tests/DocumentShareTests.cs`)

---

## Phase 6: Background Scanning & Async Pipeline (Production optional)

**Goal**: Support async scanning via queue and worker (Azure Functions). Training keeps simulated scanning; production can enable async scans.

- [ ] T029 Add a queue message schema and enqueue logic in upload flow: message contains `{ DocumentId, StorageKey, UploadedBy, UploadedAt }` (`Services/DocumentService.cs`)
- [ ] T030 Document Azure Function worker contract and add example message handler stub (`docs/azure-functions/scan-worker.md`, `examples/ScanWorkerStub.cs`)
- [ ] T031 Add configuration flags (`UseAsyncScanning`) and local Azurite guidance in `quickstart.md` and `appsettings.json` (`specs/001-document-upload-management/quickstart.md`, `appsettings.json`)

---

## Phase 7: Polish & Cross-Cutting Concerns

- [ ] T032 [P] Add UI/UX polish and accessibility checks for upload and list pages (`wwwroot/css/site.css`, `Pages/Documents/*.razor`)
- [ ] T033 [P] Add documentation updates and developer notes (`docs/document-upload.md`, `specs/001-document-upload-management/quickstart.md`)
- [ ] T034 [P] Add performance tuning and caching for document lists (`Services/DocumentService.cs`)
- [ ] T035 [P] Add security hardening: CSP review, file scanning policy docs, and logging review (`docs/security.md`)

---

## Dependencies & Execution Order

- Foundation (Phase 2) MUST complete before User Story phases begin.
- User Story 1 (Upload) should be implemented first as MVP; User Stories 2 and 3 can be developed in parallel once foundational work is complete.

## Parallel Opportunities

- Tasks marked `[P]` can be worked on in parallel by different developers (setup, validation, helpers, basic services, tests scaffolding).

## Implementation Strategy

- Deliver MVP: T001–T018 to validate upload end-to-end first. Then add browsing/search (T019–T023) and sharing (T024–T028). Finally add optional async scanning and polish tasks.

## Task Counts

- Total tasks: 35
- Tasks by story: US1: 6 (T013–T018), US2: 5 (T019–T023), US3: 5 (T024–T028)
