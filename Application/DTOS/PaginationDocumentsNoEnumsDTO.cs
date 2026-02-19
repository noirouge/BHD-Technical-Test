using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class PaginationDocumentsNoEnumsDTO
    {
        [JsonPropertyName("totalDocuments")]
        public int TotalDocuments { get; set; }
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("pageLimit")]
        public int PageLimit { get; set; }
        [JsonPropertyName("totalDocumentsInPage")]
        public int TotalDocumentsInPage { get; set; }
        [JsonPropertyName("results")]
        public List<DocumentAssetNoEnumsDTO> Results { get; set; } = [];
    }
}
