# Data Model: Document Upload and Management

## Entities

- **Document**
  - `DocumentId` int (PK)
  - `Title` string (required)
  - `Description` string (optional)
  - `Category` string (required)
  - `Tags` string (nullable) — simple comma-separated or JSON array
  - `FilePath` string (required) — relative path or storage key
  - `FileType` string (length 255)
  - `FileSize` long
  - `UploadedByUserId` int (FK -> User)
  - `UploadedAt` datetime
  - `ProjectId` int? (nullable FK -> Project)
  - `IsDeleted` bool (soft-delete)
  - `DeletedAt` datetime? (nullable)
  - `Scanned` bool
  - `ScanResult` string (e.g., "Simulated")

- **DocumentShare**
  - `DocumentShareId` int (PK)
  - `DocumentId` int (FK)
  - `SharedWithUserId` int? (nullable)
  - `SharedWithTeamId` int? (nullable)
  - `SharedByUserId` int (FK)
  - `SharedAt` datetime

## Indexes & Constraints

- Index on `UploadedByUserId` for quick user listing
- Index on `ProjectId` for project document queries
- Full-text or tag index option for faster search (optional for training)

## Migrations

- Add `Documents` table with fields above
- Add `DocumentShares` table
