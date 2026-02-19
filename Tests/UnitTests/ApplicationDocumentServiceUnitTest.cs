//using Application.Services;
using Application.DTOS;
using Application.Services;
using Domain.Entities;
using Domain.ENUMS;
using Domain.Interfaces;
using Moq;
using System.Text;
//using Application.Services;

namespace Tests;


public class ApplicationDocumentServiceUnitTest
{

    private readonly Mock<IDocumentRepository> _repositoryMock;

    private readonly DocumentService _documentService;

    public ApplicationDocumentServiceUnitTest()
    {
        _repositoryMock = new Mock<IDocumentRepository>();
        _documentService = new DocumentService(_repositoryMock.Object);
    }



    [Fact]
    public async Task UploadDocumentValidRequestReturnsId()
    {
        var request = new DocumentUploadRequestDTO
        {
            Filename = "ejemplo.pdf",
            EncodedFile = Convert.ToBase64String(Encoding.UTF8.GetBytes("datos de ejemplo en pdf")),
            ContentType = "application/pdf",
            DocumentType = DocumentType.CONTRACT,
            Channel = Channel.DIGITAL,
        };

        _repositoryMock.Setup(r => r.SaveDocument(It.IsAny<DocumentAsset>()))
            .Returns((DocumentAsset doc) => Task.FromResult(doc));

        _repositoryMock.Setup(r => r.GetAllDocuments())
    .ReturnsAsync(new List<DocumentAsset>());

        var result = await _documentService.UploadDocument(request);

        Assert.NotNull(result);
        Assert.NotNull(result.Id);
        _repositoryMock.Verify(r => r.SaveDocument(It.IsAny<DocumentAsset>()), Times.Once);
    }

    [Fact]
    public async Task GetDocumentsWithSortByReturnDocuments()
    {
        var documents = new List<DocumentAsset>
        {
      new DocumentAsset
            {
                Id = "123",
                Filename = "ejemplo.pdf",
                ContentType = "application/pdf",
                DocumentType = DocumentType.CONTRACT,
                Channel = Channel.DIGITAL,
                Status = DocumentStatus.SENT,
                Size = 1024,
                UploadDate = DateTime.UtcNow,
                Url = "1/2"
            }
        };

        var paremeters = new ParametersDTO
        {
            sortBy = SortBy.Filename,
            page = 1,
            pagesLimit = 10,
        };

        _repositoryMock.Setup(r => r.GetAllDocuments())
            .ReturnsAsync(documents);

        var result = await _documentService.GetAllDocumentsByParameters(paremeters);

        Assert.NotNull(result);
        Assert.Single(result.Results);
        Assert.Equal("ejemplo.pdf", result.Results.First().Filename);

    }
}
