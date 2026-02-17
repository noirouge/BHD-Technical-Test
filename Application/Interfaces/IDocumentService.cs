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
        Task<DocumentUploadResponseDTO> UploadDocumentAsync(DocumentUploadRequestDTO request);

        Task<List<DocumentAsset>> GetAll();

        Task<List<DocumentAsset>> GetAllByParameters(ParametersDTO parameters);

    }
}
