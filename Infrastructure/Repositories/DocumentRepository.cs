using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class DocumentRepository :  IDocumentRepository
    {
        private readonly List<DocumentAsset> documents = [];

        public Task SaveAsync(DocumentAsset document)
        {
            documents.Add(document);
            return Task.CompletedTask;
        }

        public Task<List<DocumentAsset>> GetAll()
        {
            return Task.FromResult(documents);
        }


    }
}
