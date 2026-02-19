namespace Test;

using Domain.ENUMS;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using Application.DTOS;
using System.Net.Http.Json;
using System.Net;
using Xunit.Abstractions;

public class DocumentsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    // CONSTS
    private readonly string APIUPLOADURL = "/api/bhd/mgmt/1/documents/actions/upload";
    private readonly string APIDOCUMENTSURL = "/api/bhd/mgmt/1/documents";
    private readonly string EXAMPLEBASE64 = Convert.ToBase64String(Encoding.UTF8.GetBytes("datos de ejemplo en pdf"));

    public DocumentsControllerIntegrationTests(WebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper) { 
        _factory = factory;
        _client = _factory.CreateClient();
        _output = testOutputHelper;
    }

    [Fact]
    public async Task UploadDocumentValidRequestReturnsAccepted()
    {

        var request = new DocumentUploadRequestDTO
        {
            Filename = "ejemplo.pdf",
            EncodedFile = Convert.ToBase64String(Encoding.UTF8.GetBytes("datos de ejemplo en pdf")),
            ContentType = "application/pdf",
            DocumentType = DocumentType.CONTRACT,
            Channel = Channel.DIGITAL,
        };

        var response = await _client.PostAsJsonAsync(APIUPLOADURL, request);

        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<DocumentUploadResponseDTO>();
        Assert.NotNull(result);
        Assert.NotNull(result.Id);

    }

    [Fact]
    public async Task UploadDocumentInvalidBase64ReturnsBadRequest()
    {
        var request = new DocumentUploadRequestDTO
        {
            Filename = "ejemplo.pdf",
            EncodedFile = "no soy un base64",
            ContentType = "application/pdf",
            DocumentType = DocumentType.KYC,
            Channel = Channel.BRANCH
        };
        var response = await _client.PostAsJsonAsync(APIUPLOADURL, request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    }

    [Fact]
    public async Task UploadDocumentEmptyFilenameReturnsBadRequest()
    {
        var request = new DocumentUploadRequestDTO
        {
            Filename = "",
            EncodedFile = EXAMPLEBASE64,
            ContentType = "application/pdf",
            DocumentType = DocumentType.KYC,
            Channel = Channel.BRANCH
        };
        var response = await _client.PostAsJsonAsync(APIUPLOADURL, request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    }

    [Fact]
    public async Task UploadDocumentInvalidContentTypeReturnsBadRequest()
    {
        var request = new DocumentUploadRequestDTO
        {
            Filename = "ejemplo.pdf",
            EncodedFile = EXAMPLEBASE64,
            ContentType = "pdf",
            DocumentType = DocumentType.KYC,
            Channel = Channel.BRANCH
        };
        var response = await _client.PostAsJsonAsync(APIUPLOADURL, request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    }

    [Fact]
    public async Task UploadDocumentInvalidChannelReturnsBadRequest()
    {
        var request = new /*DocumentUploadRequestDTO*/
        {
            Filename = "ejemplo.pdf",
            EncodedFile = EXAMPLEBASE64,
            ContentType = "application/pdf",
            DocumentType = DocumentType.KYC,
            Channel = 10
        };
        var response = await _client.PostAsJsonAsync(APIUPLOADURL, request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    }

    [Fact]
    public async Task UploadDocumentInvalidDocumentTypeReturnsBadRequest()
    {
        var request = new /*DocumentUploadRequestDTO*/
        {
            Filename = "ejemplo.pdf",
            EncodedFile = EXAMPLEBASE64,
            ContentType = "application/pdf",
            DocumentType = 10,
            Channel = Channel.BACKOFFICE
        };
        var response = await _client.PostAsJsonAsync(APIUPLOADURL, request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    }

    [Fact]

    public async Task GetDocumentsWithoutSortByReturnsBadRequest()
    {
        var response = await _client.GetAsync(APIDOCUMENTSURL);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]

    public async Task GetDocumentsWithSortByReturnsOk()
    {
        var request = new DocumentUploadRequestDTO
        {
            Filename = "ejemplo.pdf",
            EncodedFile = EXAMPLEBASE64,
            ContentType = "application/pdf",
            DocumentType = DocumentType.SUPPORTING_DOCUMENT,
            Channel = Channel.BACKOFFICE
        };
         await _client.PostAsJsonAsync(APIUPLOADURL, request);
        await Task.Delay(100);

        var response = await _client.GetAsync($"{APIDOCUMENTSURL}?sortBy=Filename");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var jsonContent = await response.Content.ReadAsStringAsync();
        //_output.WriteLine("Response JSON");
        //_output.WriteLine(jsonContent);
        var paginationDocuments = await response.Content.ReadFromJsonAsync<PaginationDocumentsNoEnumsDTO>();
        Assert.NotNull(paginationDocuments);
        Assert.True(paginationDocuments.TotalDocuments > 0);
        Assert.True(paginationDocuments.Results.Count > 0);
    }


}
