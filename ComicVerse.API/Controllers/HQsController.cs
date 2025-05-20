using ComicVerse.Application.DTOs;
using ComicVerse.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComicVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HQsController : ControllerBase
    {
        private readonly IHQService _hqService;
        private readonly ILogger<HQsController> _logger;

        public HQsController(IHQService hqService, ILogger<HQsController> logger)
        {
            _hqService = hqService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HQDTO>>> GetAll()
        {
            var hqs = await _hqService.GetAll();
            return Ok(hqs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HQDTO>> GetById(Guid id)
        {
            var hq = await _hqService.GetById(id);
            if (hq == null)
            {
                return NotFound();
            }
            return Ok(hq);
        }

        [HttpPost]
        public async Task<ActionResult<HQDTO>> Create(CreateHQDTO hqDto)
        {
            try
            {
                var hq = await _hqService.Add(hqDto);
                return CreatedAtAction(nameof(GetById), new { id = hq.Id }, hq);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar HQ");
                return BadRequest("Erro ao criar HQ");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateHQDTO hqDto)
        {
            try
            {
                await _hqService.Update(id, hqDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar HQ");
                return BadRequest("Erro ao atualizar HQ");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _hqService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar HQ");
                return BadRequest("Erro ao deletar HQ");
            }
        }

        [HttpPost("{hqId}/edicoes")]
        public async Task<IActionResult> AdicionarEdicao(Guid hqId, CreateEdicaoDTO edicaoDto)
        {
            try
            {
                await _hqService.AdicionarEdicao(hqId, edicaoDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar edição");
                return BadRequest("Erro ao adicionar edição");
            }
        }
    }
}