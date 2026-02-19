using Domain.ENUMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class DocumentAssetNoEnumsDTO
    {
        [Required]
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [Required]
        [JsonPropertyName("filename")]
        public required string Filename { get; set; }

        [Required]
        [JsonPropertyName("contentType")]
        public required string ContentType { get; set; }

        [Required]
        [JsonPropertyName("documentType")]
        public required string DocumentType { get; set; }

        [Required]
        [JsonPropertyName("channel")]
        public required string Channel { get; set; }

        [JsonPropertyName("customerId")]
        public string? CustomerId { get; set; }

        [Required]
        [JsonPropertyName("status")]
        public required string Status { get; set; }

        [Required]
        [JsonPropertyName("size")]
        public int Size { get; set; }

        [Required]
        [JsonPropertyName("uploadDate")]
        public DateTime UploadDate { get; set; }

        [Required]
        [JsonPropertyName("url")]
        public required string Url { get; set; }

        [JsonPropertyName("correlationId")]
        public string? CorrelationId { get; set; }
    }
}
