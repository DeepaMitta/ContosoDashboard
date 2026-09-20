using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoDashboard.Models
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public string Category { get; set; } = null!;

        public string? Tags { get; set; }

        [Required]
        public string FilePath { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string FileType { get; set; } = null!;

        public long FileSize { get; set; }

        public int UploadedByUserId { get; set; }

        public DateTime UploadedAt { get; set; }

        public int? ProjectId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool Scanned { get; set; }

        public string? ScanResult { get; set; }
    }
}
