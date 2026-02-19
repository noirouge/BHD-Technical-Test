using Microsoft.AspNetCore.Mvc;
using Application.DTOS;
using Application.Interfaces;
using Domain.Entities;
using System.Net.Mime;
using Domain.ENUMS;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Application.Utils;

namespace API.Controllers
{
    [ApiController]
    [Route("api/bhd/mgmt/1/documents")]
    public class DocumentsController : ControllerBase
    {


        private readonly IDocumentService _documentService;
        private readonly ILogger<DocumentsController> _logger;

        public DocumentsController(IDocumentService documentService, ILogger<DocumentsController> logger)
        {
            _documentService = documentService;
            _logger = logger;
        }


        [HttpPost("actions/upload")]
        [ProducesResponseType(typeof(DocumentUploadResponseDTO), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocumentUploadResponseDTO>> UploadDocument([FromBody] DocumentUploadRequestDTO document)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!Utils.IsBase64(document.EncodedFile))
                {
                    ModelState.AddModelError(nameof(document.ContentType), "The field encondedFile must have a correct base64 string");
                    return BadRequest(ModelState);
                }
                
                var regex = new Regex(@"^[a-zA-Z0-9!#$&^_.+-]+/[a-zA-Z0-9!#$&^_.+-]+.*");
                bool isValidContentType = regex.IsMatch(document.ContentType);

                if (!isValidContentType)
                {
                    ModelState.AddModelError(nameof(document.ContentType), "The field contentType don't have a valid MIME type");
                    return BadRequest(ModelState);
                }


                var response = await _documentService.UploadDocument(document);


                return AcceptedAtAction(nameof(UploadDocument), response);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error Saving Document");
                return StatusCode(500, new { error = "An unexpected error ocurred" });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<DocumentAsset>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<DocumentAsset>>> GetDocumentsByFilters(
            [FromQuery] DateTime? uploadDateStart,
            [FromQuery] DateTime? uploadDateEnd,
            [FromQuery] string? filename,
            [FromQuery] string? contentType,
            [FromQuery] DocumentType? documentType,
            [FromQuery] DocumentStatus? status,
            [FromQuery] string? customerId,
            [FromQuery] Channel? channel,
            [FromQuery] SortBy? sortBy,
            [FromQuery] SortDirection sortDirection = SortDirection.ASC,
            [FromQuery] int page = 1,
            [FromQuery] int pageLimit = 10
            )
        {

            try
            {

                if (!sortBy.HasValue)
                    return BadRequest(new { error = "The sortBy Param is mandatory!" });
                if (page < 1 )
                    return BadRequest("The value of page param must be equal or greater than 1");
                if (pageLimit > 50)
                    return BadRequest("The value of pageLimit param can't be greater than 50");
                else if (pageLimit < 1)
                    return BadRequest("The value of pageLimit param can't be less than 1");

                    var parameters = new ParametersDTO
                    {
                        sortBy = sortBy.Value,
                        channel = channel,
                        documentType = documentType,
                        customerId = customerId,
                        contentType = contentType,
                        filename = filename,
                        sortDirection = sortDirection,
                        status = status,
                        uploadDateEnd = uploadDateEnd,
                        uploadDateStart = uploadDateStart,
                        page = page,
                        pagesLimit = pageLimit
                    };
                string url = $"{Request.Scheme}://{Request.Host}";


                var documents = await _documentService.GetAllDocumentsByParameters(parameters);

                return Ok(documents);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error getting documents");
                return StatusCode(500, new { error = "An unexpected error ocurred" });
            }


        }


    }
}
