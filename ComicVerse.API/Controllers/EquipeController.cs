using ComicVerse.Application.DTOs;
using ComicVerse.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComicVerse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipesController : ControllerBase
    {
        private readonly IEquipeService _equipeService;
        private readonly ILogger<EquipesController> _logger;

        public EquipesController(
            IEquipeService equipeService,
            ILogger<EquipesController> logger)
        {
            _equipeService = equipeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipeDTO>>> GetAll()
        {
            var equipes = await _equipeService.GetAll();
            return Ok(equipes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipeDTO>> GetById(Guid id)
        {
            var equipe = await _equipeService.GetById(id);
            if (equipe == null)
            {
                return NotFound();
            }
            return Ok(equipe);
        }

        [HttpPost]
        public async Task<ActionResult<EquipeDTO>> Create(CreateEquipeDTO equipeDto)
        {
            try
            {
                var equipe = await _equipeService.Add(equipeDto);
                return CreatedAtAction(nameof(GetById), new { id = equipe.Id }, equipe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar equipe");
                return BadRequest("Erro ao criar equipe");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateEquipeDTO equipeDto)
        {
            try
            {
                await _equipeService.Update(id, equipeDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar equipe");
                return BadRequest("Erro ao atualizar equipe");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _equipeService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar equipe");
                return BadRequest("Erro ao deletar equipe");
            }
        }

        [HttpGet("{id}/personagens")]
        public async Task<ActionResult<IEnumerable<PersonagemDTO>>> GetPersonagens(Guid id)
        {
            try
            {
                var personagens = await _equipeService.GetPersonagens(id);
                return Ok(personagens);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar personagens da equipe");
                return BadRequest("Erro ao buscar personagens da equipe");
            }
        }
    }
}