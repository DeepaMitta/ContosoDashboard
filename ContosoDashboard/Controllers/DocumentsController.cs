using ContosoDashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoDashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentService _documentService;

        public DocumentsController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] string title, [FromForm] string category, [FromForm] string? description, [FromForm] int? projectId, [FromForm] string? tags)
        {
            if (file == null) return BadRequest("No file provided");

            // For training, derive user id from mock auth (not implemented here) - default to 4 (Ni Kang)
            var userId = 4;

            var doc = await _documentService.UploadAsync(file, title, category, description, userId, projectId, tags);
            if (doc == null) return BadRequest("Upload failed");

            return CreatedAtAction(nameof(Download), new { documentId = doc.DocumentId }, new { doc.DocumentId, doc.Title, doc.FileType, doc.FileSize, doc.UploadedAt });
        }

        [HttpGet("{documentId}/download")]
        public IActionResult Download(int documentId)
        {
            // Placeholder - implementation will validate authorization and stream file
            return NotFound();
        }
    }
}
