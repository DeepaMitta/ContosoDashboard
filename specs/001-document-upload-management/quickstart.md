# Quickstart: Validate Document Upload Feature (Training)

Prerequisites:

- .NET 8.0 SDK installed
- SQL Server LocalDB available (or adjust connection string to local SQL)

Steps:

1. Ensure you're on the feature branch `001-document-upload-management`.

2. Run the application (it will create and seed the DB if missing):

```bash
cd ContosoDashboard
dotnet run
```

3. Open the app in a browser (http://localhost:5000 by default) and login via the mock login page.

4. Navigate to Documents (new `Documents` entry in nav). Try these validation scenarios:

- Upload a small PDF (<= 5 MB): expect success, file listed in "My Documents", and preview available.
- Upload a file >25 MB: expect rejection with a clear error message.
- Upload an unsupported type (e.g., `.exe`): expect rejection.
- Share a document with a teammate: recipient should appear in "Shared with Me" and receive an in-app notification.
- Delete a document: it should appear removed from views but recoverable from admin or via a recovery flow (soft-delete).

5. Performance checks (optional): seed many documents and verify list/search responses remain under 2s for typical test machines.

Notes:
- Scanning is simulated in training; metadata includes `Scanned=true` and `ScanResult="Simulated"`.
- Files are stored outside `wwwroot` in `AppData/uploads` by default. Adjust the path in `appsettings.json` if needed.
