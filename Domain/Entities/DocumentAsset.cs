using Domain.ENUMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Enumeration;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DocumentAsset
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
        public required DocumentType DocumentType { get; set; }

        [Required]
        [JsonPropertyName("channel")]
        public Channel Channel { get; set; }

        [JsonPropertyName("customerId")]
        public string? CustomerId { get; set; }

        [Required]
        [JsonPropertyName("status")]
        public DocumentStatus Status { get; set; }

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
