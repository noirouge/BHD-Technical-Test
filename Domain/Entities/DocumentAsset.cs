using Domain.ENUMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Enumeration;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DocumentAsset
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
        public string? CustomerId {get; set;}

        [Required]
        public DocumentStatus Status { get; set; }
        //public string? Url { get; set;}

        [Required]
        public int Size { get; set;}

        [Required]
        public DateTime UploadDate { get; set;}

        public string? CorrelationId {get; set;}
        //public byte[] EncodedFile { get; set; }

    }
}
