# Research: Document Upload and Management (Phase 0)

## Clarifications Resolved

1. Decision: Virus scanning for training — Simulated

- Rationale: The ContosoDashboard repository is offline-first and used for training. Simulating the scan avoids external dependencies and complex setup for learners while still teaching the scanning step and the production requirement. Implement uploads to set `Scanned=true` and `ScanResult="Simulated"` in metadata; document production integration options (ClamAV, cloud API).
- Alternatives considered:
  - Local scanner (ClamAV): realistic but increases setup complexity for students and maintainers.
  - Cloud scanning API: realistic but breaks offline-first requirement and needs API keys.

2. Decision: Retention policy — Soft-delete with 30-day recovery

- Rationale: Soft-delete provides a safe training experience (recover accidental deletes) and demonstrates retention/recovery workflows without risking permanent data loss. It also keeps implementation straightforward: add `IsDeleted` + `DeletedAt` fields and a periodic cleanup job (documented but optional for training). Permanent purge after 30 days is configurable and may be run manually in training.
- Alternatives considered:
  - No automatic retention: simplest but risks accidental permanent loss in training exercises.
  - Auto-delete after X years: realistic for compliance but unnecessary for training and adds complexity.

## Implementation Research Notes

- Local file layout: `AppData/uploads/{userId}/{projectId|personal}/{guid}.{ext}` — consistent with spec and migration to blob storage.
- Storage abstraction: `IFileStorageService` interface (UploadAsync, DeleteAsync, DownloadAsync, GetUrlAsync) with `LocalFileStorageService` implementation.
- Security: serve files through an authorized endpoint that performs authorization checks against project membership and `DocumentShare` records; never expose raw filesystem paths.
- Testing: unit tests for `DocumentService` business logic; bUnit for upload UI component basic flows; integration test to verify upload → file present → metadata saved → authorized download.

## Open Questions (no blockers)

- None outstanding; decisions above resolve prior [NEEDS CLARIFICATION] markers.
