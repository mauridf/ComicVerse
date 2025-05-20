using ComicVerse.Application.DTOs;
using ComicVerse.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComicVerse.API.Controllers
{
    [ApiController]
    [Route("api/hqs/{hqId}/[controller]")]
    public class EdicoesController : ControllerBase
    {
        private readonly IEdicaoService _edicaoService;
        private readonly ILogger<EdicoesController> _logger;

        public EdicoesController(
            IEdicaoService edicaoService,
            ILogger<EdicoesController> logger)
        {
            _edicaoService = edicaoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EdicaoDTO>>> GetByHQId(Guid hqId)
        {
            var edicoes = await _edicaoService.GetByHQId(hqId);
            return Ok(edicoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EdicaoDTO>> GetById(Guid hqId, Guid id)
        {
            var edicao = await _edicaoService.GetById(id);
            if (edicao == null || edicao.HQId != hqId)
            {
                return NotFound();
            }
            return Ok(edicao);
        }

        [HttpPost]
        public async Task<ActionResult<EdicaoDTO>> Create(Guid hqId, CreateEdicaoDTO edicaoDto)
        {
            try
            {
                var edicao = await _edicaoService.Add(hqId, edicaoDto);
                return CreatedAtAction(nameof(GetById), new { hqId, id = edicao.Id }, edicao);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("HQ não encontrada");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar edição");
                return BadRequest("Erro ao criar edição");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid hqId, Guid id, UpdateEdicaoDTO edicaoDto)
        {
            try
            {
                await _edicaoService.Update(id, edicaoDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar edição");
                return BadRequest("Erro ao atualizar edição");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid hqId, Guid id)
        {
            try
            {
                await _edicaoService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar edição");
                return BadRequest("Erro ao deletar edição");
            }
        }

        [HttpPatch("{id}/marcar-como-lida")]
        public async Task<IActionResult> MarcarComoLida(Guid hqId, Guid id, [FromBody] bool lida)
        {
            try
            {
                await _edicaoService.MarcarComoLida(id, lida);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao marcar edição como lida");
                return BadRequest("Erro ao marcar edição como lida");
            }
        }
    }
}