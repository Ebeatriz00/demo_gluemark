using Application.DTOs.Currency;
using Application.UseCases.Currency;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlueMark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly CreateCurrency _createCurrency;
        private readonly GetAllCurrency _getAllCurrencys;
        private readonly GetByIdCurrency _getByIdCurrencys;
        private readonly UpdateCurrency _updateCurrencys;
        private readonly PatchCurrencyStatus _patchCurrencyStatus;

        public CurrencyController(CreateCurrency createCurrency, GetAllCurrency getAllCurrencys, GetByIdCurrency getByIdCurrencys, UpdateCurrency updateCurrencys, PatchCurrencyStatus patchCurrencyStatus)
        {
            _createCurrency = createCurrency;
            _getAllCurrencys = getAllCurrencys;
            _getByIdCurrencys = getByIdCurrencys;
            _updateCurrencys = updateCurrencys;
            _patchCurrencyStatus = patchCurrencyStatus;
        }

        [HttpPost]
        [Route("CurrencyCreate")]
        public async Task<IActionResult> Create([FromBody] CurrencyCreateDto dto)
        {
            var result = await _createCurrency.ExecuteAsync(dto);
            return Ok(result);
        }
        [HttpGet]
        [Route("CurrencyList")]
        public async Task<IActionResult> GetList([FromQuery] int business_id)
        {
            var result = await _getAllCurrencys.ExecuteAsync(business_id);
            if (result == null || !result.Any())
                return NotFound(new { message = "No se encontraron tipos de documentos." });

            return Ok(result);
        }

        [HttpGet]
        [Route("CurrencyIdList")]
        public async Task<IActionResult> GetListId([FromQuery] int CurrencyId)
        {
            var result = await _getByIdCurrencys.ExecuteAsync(CurrencyId);
            if (result == null)
                return NotFound(new { message = "No se encontraron tipos de documentos." });

            return Ok(result);
        }
        [HttpPut]
        [Route("CurrencyUpdate")]
        public async Task<IActionResult> Update([FromBody] CurrencyUpdateDto dto)
        {
            var result = await _updateCurrencys.ExecuteAsync(dto);
            return Ok(result);
        }

        [HttpPatch]
        [Route("CurrencyUpdateStatus")]
        public async Task<IActionResult> Patch([FromBody] CurrencyStatusToggleDto dto)
        {
            var result = await _patchCurrencyStatus.ExecuteAsync(dto);
            return Ok(result);
        }


    }
}
