using Microsoft.AspNetCore.Mvc;
using Application.DTOS;
using Application.Interfaces;
using Domain.Entities;
using System.Net.Mime;
using Domain.ENUMS;

namespace API.Controllers
{
    [ApiController]
    [Route("api/bhd/mgmt/1/documents")]
    public class DocumentsController : ControllerBase
    {


        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }


        [HttpPost("actions/upload")]
        [ProducesResponseType(typeof(DocumentUploadResponseDTO), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocumentUploadResponseDTO>> Post([FromBody] DocumentUploadRequestDTO document)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _documentService.UploadDocumentAsync(document);

                return AcceptedAtAction(nameof(Post), response);
            }
            catch (FormatException ex)
            {
                return BadRequest(new { error = "Invalid base64 encoded file format" });
            }
            catch (Exception ex) {
                return StatusCode(500, new { error = "An unexpected error ocurred" });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<DocumentAsset>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<DocumentAsset>>> Get(
            [FromQuery] DateTime? uploadDateStart,
            [FromQuery] DateTime? uploadDateEnd,
            [FromQuery] string? filename,
            [FromQuery] string? contentType,
            [FromQuery] DocumentType? documentType,
            [FromQuery] DocumentStatus? status,
            [FromQuery] string? customerId,
            [FromQuery] Channel? channel,
            [FromQuery] SortBy sortBy,
            [FromQuery] SortDirection sortDirection = SortDirection.ASC
            )
        {

            var documents = await _documentService.GetAll();

            return Ok(documents);
        }

    }
}
