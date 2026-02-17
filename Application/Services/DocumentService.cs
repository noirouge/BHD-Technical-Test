using Application.DTOS;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ENUMS;
using Domain.Interfaces;

namespace Application.Services
{
    public class DocumentService : IDocumentService
    {

        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }


      public async Task<DocumentUploadResponseDTO> UploadDocumentAsync(DocumentUploadRequestDTO request)
        {

            var encodedBytes = Convert.FromBase64String(request.EncondedFile);

            var document = new DocumentAsset
            {
                Id = Guid.NewGuid().ToString(),
                Filename = request.Filename,
                ContentType = request.ContentType,
                DocumentType = request.DocumentType,
                Channel = request.Channel,
                CustomerId = request.CustomerId,
                Status = DocumentStatus.RECEIVED,
                Size = encodedBytes.Length,
                UploadDate = DateTime.UtcNow,
                CorrelationId = request.CorrelationId,
                EncodedFile = encodedBytes,
            };

            await _documentRepository.SaveAsync(document);

            return new DocumentUploadResponseDTO { Id = document.Id };
        }

        public async Task<List<DocumentAsset>> GetAll()
        {

            return await _documentRepository.GetAll();
        }

        public async Task<List<DocumentAsset>> GetAllByParameters(ParametersDTO parameters)
        {
            var documents = await _documentRepository.GetAll();

            switch (parameters.sortBy)
            {
                case SortBy.Filename:
                    //documents = documents.OrderBy(doc => doc.Filename);
                    break;
            }


            //if (parameters.sortDirection == SortDirection.ASC)
            //    documents.OrderByDescending();
            
            


            return documents;
        }

    }
}
