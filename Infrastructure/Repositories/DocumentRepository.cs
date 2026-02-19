using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DocumentRepository :  IDocumentRepository
    {

        private readonly DocumentsDbContext _dbContext;

        public DocumentRepository(DocumentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveDocument(DocumentAsset document)
        {
            await _dbContext.documents.AddAsync(document);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<DocumentAsset>> GetAllDocuments()
        {
            var documents = await _dbContext.documents.ToListAsync();
            return documents;
        }

        public async Task<DocumentAsset?> GetDocumentById(string id)
        {
            var document = await _dbContext.documents.SingleOrDefaultAsync(x => x.Id == id);
     
                return document;
        }

      


    }
}
