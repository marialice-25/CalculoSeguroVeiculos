using CalculoSeguroVeiculos.Application.DTOs;
using CalculoSeguroVeiculos.Application.Interfaces;
using CalculoSeguroVeiculos.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CalculoSeguroVeiculos.API.Controllers
{
    [ApiController]
    [Route("api/seguros")]
    public class SeguroController : ControllerBase
    {
        private readonly ISeguroService _seguroService;

        public SeguroController(ISeguroService seguroService)
        {
            _seguroService = seguroService;
        }

        [HttpPost]
        public async Task<IActionResult> CalcularSeguro([FromBody] SeguroDto dto)
        {
            var seguro = await _seguroService.CalcularSeguroAsync(dto);
            return Ok(seguro);
        }

        [HttpGet("todos")]
        public async Task<IActionResult> GetSeguros()
        {
            var seguros = await _seguroService.GetSeguros();
            return Ok(seguros);
        }

        [HttpGet("pesquisar")]
        public async Task<IActionResult> PesquisarSeguro([FromQuery] string termo)
        {
            if (string.IsNullOrEmpty(termo))
                return BadRequest("O termo de pesquisa não pode ser vazio");

            var seguros = await _seguroService.PesquisarSeguroAsync(termo);

            if (seguros == null || seguros.Count == 0)
                return NotFound("Nenhum seguro encontrado");

            return Ok(seguros);
        }
    }
}
