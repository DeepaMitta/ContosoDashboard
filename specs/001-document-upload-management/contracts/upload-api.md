# Contracts: Upload & Download Endpoints

These contracts describe the HTTP endpoints and payloads for uploading, downloading, and managing documents. They are intended as internal API contracts for the Blazor Server app and for any future service extraction.

## Upload (Form POST)

- Endpoint: `POST /api/documents/upload`
- Auth: Cookie-based auth; must be authenticated
- Body: multipart/form-data
  - `file` (one or more) — file bytes
  - `title` (string, required)
  - `description` (string, optional)
  - `category` (string, required)
  - `projectId` (int, optional)
  - `tags` (string, optional)
- Response: 201 Created with JSON body `{ documentId, title, fileType, fileSize, uploadedAt }` or 4xx on validation/authorization errors

## Download

- Endpoint: `GET /api/documents/{documentId}/download`
- Auth: Cookie-based auth; server validates authorization for `documentId` via project membership or `DocumentShare` records
- Response: 200 OK with file stream and `Content-Type` set to `FileType`; 403 if unauthorized; 404 if not found

## Preview

- Endpoint: `GET /api/documents/{documentId}/preview`
- Behavior: same as download but optimized for inline display (Content-Disposition: inline)

## Metadata Update

- Endpoint: `PATCH /api/documents/{documentId}`
- Body: JSON with allowed fields `{ title, description, category, tags }`
- Authz: only uploader or authorized role (ProjectManager/Admin)

## Delete

- Endpoint: `DELETE /api/documents/{documentId}`
- Behavior: soft-delete (sets `IsDeleted=true`, `DeletedAt=now`) for training; purge job can permanently remove items older than 30 days
- Authz: uploader, ProjectManager for project, or Admin
