using ComicVerse.Application.DTOs;
using ComicVerse.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComicVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonagensController : ControllerBase
    {
        private readonly IPersonagemService _personagemService;
        private readonly ILogger<PersonagensController> _logger;

        public PersonagensController(
            IPersonagemService personagemService,
            ILogger<PersonagensController> logger)
        {
            _personagemService = personagemService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonagemDTO>>> GetAll()
        {
            var personagens = await _personagemService.GetAll();
            return Ok(personagens);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonagemDTO>> GetById(Guid id)
        {
            var personagem = await _personagemService.GetById(id);
            if (personagem == null)
            {
                return NotFound();
            }
            return Ok(personagem);
        }

        [HttpPost]
        public async Task<ActionResult<PersonagemDTO>> Create(CreatePersonagemDTO personagemDto)
        {
            try
            {
                var personagem = await _personagemService.Add(personagemDto);
                return CreatedAtAction(nameof(GetById), new { id = personagem.Id }, personagem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar personagem");
                return BadRequest("Erro ao criar personagem");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePersonagemDTO personagemDto)
        {
            try
            {
                await _personagemService.Update(id, personagemDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar personagem");
                return BadRequest("Erro ao atualizar personagem");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _personagemService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar personagem");
                return BadRequest("Erro ao deletar personagem");
            }
        }

        [HttpGet("{id}/equipes")]
        public async Task<ActionResult<IEnumerable<EquipeDTO>>> GetEquipes(Guid id)
        {
            var personagem = await _personagemService.GetById(id);
            if (personagem == null)
            {
                return NotFound();
            }
            return Ok(personagem.Equipes);
        }
    }
}