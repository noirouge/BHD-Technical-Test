using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDocumentRepository
    {
        Task SaveDocument(DocumentAsset document);
        Task<List<DocumentAsset>> GetAllDocuments();

        Task<DocumentAsset?> GetDocumentById(string id);
    }
}
