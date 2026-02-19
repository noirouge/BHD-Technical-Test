using Application.DTOS;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDocumentService
    {
        Task<DocumentUploadResponseDTO> UploadDocument(DocumentUploadRequestDTO request);

        Task<List<DocumentAsset>> GetAllDocuments();

        Task<PaginationDocumentsDTO> GetAllDocumentsByParameters(ParametersDTO parameters);


    }
}
