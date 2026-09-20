using ContosoDashboard.Data;
using ContosoDashboard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ContosoDashboard.Services
{
    public class DocumentService
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileStorageService _storage;
        private readonly ILogger<DocumentService> _logger;

        public DocumentService(ApplicationDbContext db, IFileStorageService storage, ILogger<DocumentService> logger)
        {
            _db = db;
            _storage = storage;
            _logger = logger;
        }

        public async Task<Document?> UploadAsync(IFormFile file, string title, string category, string? description, int uploadedByUserId, int? projectId, string? tags)
        {
            if (file == null || file.Length == 0) return null;

            // Basic validation
            if (file.Length > 25 * 1024 * 1024) throw new InvalidOperationException("File exceeds size limit");

            var guid = Guid.NewGuid().ToString();
            var ext = Path.GetExtension(file.FileName);
            var relativePath = Path.Combine(uploadedByUserId.ToString(), projectId?.ToString() ?? "personal", guid + ext);

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            ms.Position = 0;

            await _storage.UploadAsync(ms, relativePath);

            var doc = new Document
            {
                Title = title,
                Description = description,
                Category = category,
                Tags = tags,
                FilePath = relativePath.Replace("\\", "/"),
                FileType = file.ContentType ?? "application/octet-stream",
                FileSize = file.Length,
                UploadedByUserId = uploadedByUserId,
                UploadedAt = DateTime.UtcNow,
                ProjectId = projectId,
                Scanned = true,
                ScanResult = "Simulated"
            };

            _db.Documents.Add(doc);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Uploaded document {DocumentId} by user {UserId}", doc.DocumentId, uploadedByUserId);
            return doc;
        }
    }
}
