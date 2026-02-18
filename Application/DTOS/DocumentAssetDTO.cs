using Domain.ENUMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class DocumentAssetDTO
    {

        public string Id { get; set; }

        [Required]
        public required string Filename { get; set; }

        [Required]
        public required string ContentType { get; set; }

        [Required]
        public required DocumentType DocumentType { get; set; }

        [Required]
        public Channel Channel { get; set; }
        public string? CustomerId { get; set; }

        [Required]
        public DocumentStatus Status { get; set; }
        public string? Url { get; set; }

        [Required]
        public int Size { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        public string? CorrelationId { get; set; }
    }
}
