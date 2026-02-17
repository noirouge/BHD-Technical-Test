using Domain.ENUMS;
using System;
using System.Collections.Generic;
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
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public DocumentType DocumentType { get; set; }
        public Channel Channel { get; set; }
        public string? CustomerId {get; set;}
        public DocumentStatus Status { get; set; }
        public string? Url { get; set;}
        public int Size { get; set;}
        public DateTime UploadDate { get; set;}

        public string? CorrelationId {get; set;}
        public byte[] EncodedFile { get; set; }

    }
}
