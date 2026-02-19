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


        private async Task<string> SaveLocalFile(DocumentUploadRequestDTO request)
        {
            byte[] fileBytes = Convert.FromBase64String(request.EncodedFile);

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            string docName = request.Filename;

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var documents = await _documentRepository.GetAllDocuments();

            var existDocSameName = documents.Where(doc => doc.Filename == request.Filename).FirstOrDefault();

            if (existDocSameName != null)
                docName = $"{Guid.NewGuid().ToString()}-{docName}";

            string fullPath = Path.Combine(folderPath, docName);

            await File.WriteAllBytesAsync(fullPath, fileBytes);

            return fullPath.Replace("\\", "/");
        }

      public async Task<DocumentUploadResponseDTO> UploadDocument(DocumentUploadRequestDTO request)
        {

            byte[] fileBytes = Convert.FromBase64String(request.EncodedFile);

            string url = await SaveLocalFile(request);

            

            var document = new DocumentAsset
            {
                Id = Guid.NewGuid().ToString(),
                Filename = request.Filename,
                ContentType = request.ContentType,
                DocumentType = request.DocumentType,
                Channel = request.Channel,
                CustomerId = request.CustomerId,
                Status = DocumentStatus.RECEIVED,
                Size = fileBytes.Length,
                UploadDate = DateTime.UtcNow,
                CorrelationId = request.CorrelationId,
                Url = url
            };

            await _documentRepository.SaveDocument(document);

            return new DocumentUploadResponseDTO { Id = document.Id };
        }

        public async Task<List<DocumentAsset>> GetAllDocuments()
        {

            return await _documentRepository.GetAllDocuments();
        }


        private List<DocumentAsset> DocumentsOrderBy(List<DocumentAsset> documents, SortBy sortBy, SortDirection sortDirection)
        {
            var documentsOrdered = documents;

            switch (sortBy)
            {
                case SortBy.Filename:
                    if(sortDirection == SortDirection.DESC)
                        documentsOrdered = documentsOrdered.OrderByDescending(document => document.Filename).ToList();
                        else
                        documentsOrdered = documentsOrdered.OrderBy(document => document.Filename).ToList();
                    break;
                case SortBy.UploadDate:
                    if (sortDirection == SortDirection.DESC)
                        documentsOrdered = documentsOrdered.OrderByDescending(document => document.UploadDate).ToList();
                    else
                        documentsOrdered = documentsOrdered.OrderBy(document => document.UploadDate).ToList();
                    break;
                case SortBy.Status:
                    if (sortDirection == SortDirection.DESC)
                        documentsOrdered = documentsOrdered.OrderByDescending(document => document.Status).ToList();
                    else
                        documentsOrdered = documentsOrdered.OrderBy(document => document.Status).ToList();
                    break;
                case SortBy.DocumentType:
                    if (sortDirection == SortDirection.DESC)
                        documentsOrdered = documentsOrdered.OrderByDescending(document => document.DocumentType).ToList();
                    else
                        documentsOrdered = documentsOrdered.OrderBy(document => document.DocumentType).ToList();
                    break;
            }

            return documentsOrdered;
        }

    

        public async Task<PaginationDocumentsDTO> GetAllDocumentsByParameters(ParametersDTO parameters)
        {
            var documents = await _documentRepository.GetAllDocuments();

           var documentsSortered = DocumentsOrderBy(documents, parameters.sortBy, parameters.sortDirection);

            if (parameters.uploadDateStart.HasValue)
                documentsSortered = documentsSortered.Where(doc => doc.UploadDate >= parameters.uploadDateStart).ToList();
            if (parameters.uploadDateEnd.HasValue)
                documentsSortered = documentsSortered.Where(doc => doc.UploadDate <= parameters.uploadDateEnd).ToList();
            if ( !string.IsNullOrWhiteSpace(parameters.filename))
                documentsSortered = documentsSortered.Where(doc => doc.Filename.ToLower().Contains(parameters.filename.ToLower())).ToList();
            if (!string.IsNullOrWhiteSpace(parameters.contentType))
                documentsSortered = documentsSortered.Where(doc => doc.ContentType == parameters.contentType).ToList();
            if (parameters.documentType.HasValue)
                documentsSortered =documentsSortered.Where(doc => doc.DocumentType == parameters.documentType).ToList();
            if(parameters.status.HasValue)
                documentsSortered = documentsSortered.Where(doc => doc.Status == parameters.status).ToList();
            if(!string.IsNullOrWhiteSpace(parameters.customerId))
                documentsSortered = documentsSortered.Where(doc => doc.CustomerId == parameters.customerId).ToList();
            if (parameters.channel.HasValue)
                documentsSortered = documentsSortered.Where(doc => doc.Channel == parameters.channel).ToList();

            var documentsByPage = documentsSortered.Skip((parameters.page-1)*parameters.pagesLimit).Take(parameters.pagesLimit).ToList();


            var paginationDocuments = new PaginationDocumentsDTO {
            TotalDocuments = documentsSortered.Count(),
            Page = parameters.page,
            PageLimit = parameters.pagesLimit,
            TotalDocumentsInPage = documentsByPage.Count(),
            Results = documentsByPage
            };

            return paginationDocuments;
        }

    }
}
