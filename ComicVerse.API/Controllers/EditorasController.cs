using ComicVerse.Application.DTOs;
using ComicVerse.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComicVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EditorasController : ControllerBase
    {
        private readonly IEditoraService _editoraService;
        private readonly ILogger<EditorasController> _logger;

        public EditorasController(
            IEditoraService editoraService,
            ILogger<EditorasController> logger)
        {
            _editoraService = editoraService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EditoraDTO>>> GetAll()
        {
            var editoras = await _editoraService.GetAll();
            return Ok(editoras);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EditoraDTO>> GetById(Guid id)
        {
            var editora = await _editoraService.GetById(id);
            if (editora == null)
            {
                return NotFound();
            }
            return Ok(editora);
        }

        [HttpPost]
        public async Task<ActionResult<EditoraDTO>> Create(CreateEditoraDTO editoraDto)
        {
            try
            {
                var editora = await _editoraService.Add(editoraDto);
                return CreatedAtAction(nameof(GetById), new { id = editora.Id }, editora);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar editora");
                return BadRequest("Erro ao criar editora");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateEditoraDTO editoraDto)
        {
            try
            {
                await _editoraService.Update(id, editoraDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar editora");
                return BadRequest("Erro ao atualizar editora");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _editoraService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar editora");
                return BadRequest("Erro ao deletar editora");
            }
        }
    }
}