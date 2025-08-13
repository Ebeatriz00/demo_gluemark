using Application.DTOs.DocumentType;
using Application.UseCases.DocumentType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlueMark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly CreateDocumentType _createDocumentType;
        private readonly GetAllDocumentTypes _getAllDocumentTypes;
        private readonly GetByIdDocumentTypes _getByIdDocumentTypes;
        private readonly UpdateDocumentType _updateDocumentTypes;
        private readonly PatchDocumentTypeStatus _patchDocumentTypeStatus;

        public DocumentTypeController(CreateDocumentType createDocumentType, GetAllDocumentTypes getAllDocumentTypes, GetByIdDocumentTypes getByIdDocumentTypes, UpdateDocumentType updateDocumentTypes, PatchDocumentTypeStatus patchDocumentTypeStatus)
        {
            _createDocumentType = createDocumentType;
            _getAllDocumentTypes = getAllDocumentTypes;
            _getByIdDocumentTypes = getByIdDocumentTypes;
            _updateDocumentTypes = updateDocumentTypes;
            _patchDocumentTypeStatus = patchDocumentTypeStatus;
        }

        [HttpPost]
        [Route("DocumentTypeCreate")]
        public async Task<IActionResult> Create([FromBody] DocumentTypeCreateDto dto)
        {
            var result = await _createDocumentType.ExecuteAsync(dto);
            return Ok(result);
        }
        [HttpGet]
        [Route("DocumentTypeList")]
        public async Task<IActionResult> GetList([FromQuery] int business_id)
        {
            var result = await _getAllDocumentTypes.ExecuteAsync(business_id);
            if (result == null || !result.Any())
                return NotFound(new { message = "No se encontraron tipos de documentos." });

            return Ok(result);
        }

        [HttpGet]
        [Route("DocumentTypeIdList")]
        public async Task<IActionResult> GetListId([FromQuery] int documentTypeId)
        {
            var result = await _getByIdDocumentTypes.ExecuteAsync(documentTypeId);
            if (result == null)
                return NotFound(new { message = "No se encontraron tipos de documentos." });

            return Ok(result);
        }
        [HttpPut]
        [Route("DocumentTypeUpdate")]
        public async Task<IActionResult> Update([FromBody] DocumentTypeUpdateDto dto)
        {
            var result = await _updateDocumentTypes.ExecuteAsync(dto);
            return Ok(result);
        }

        [HttpPatch]
        [Route("DocumentTypeUpdateStatus")]
        public async Task<IActionResult> Patch([FromBody] DocumentTypeStatusToggleDto dto)
        {
            var result = await _patchDocumentTypeStatus.ExecuteAsync(dto);
            return Ok(result);
        }

    }
}
