using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ENUMS;

namespace Application.DTOS
{
    public class DocumentUploadRequestDTO
    {
        [Required]
        public string Filename { get; set; }
        [Required]
        public string EncodedFile { get; set; }
        [Required]
        public string ContentType { get; set; }
        [Required]
        [EnumDataType(typeof(DocumentType), ErrorMessage = "Invalid value for documentType field")]
        public DocumentType DocumentType { get; set; }
        [Required]
        [EnumDataType(typeof(Channel), ErrorMessage = "Invalid value for channel field")]
        public Channel Channel { get; set; }

        public string? CustomerId { get; set; }
        public string? CorrelationId { get; set; }

    }
}
