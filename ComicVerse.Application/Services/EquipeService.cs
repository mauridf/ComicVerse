using AutoMapper;
using ComicVerse.Application.DTOs;
using ComicVerse.Core.Entities;
using ComicVerse.Core.Interfaces.Repositories;

namespace ComicVerse.Application.Services
{
    public interface IEquipeService
    {
        Task<IEnumerable<EquipeDTO>> GetAll();
        Task<EquipeDTO?> GetById(Guid id);
        Task<EquipeDTO> Add(CreateEquipeDTO equipeDto);
        Task Update(Guid id, UpdateEquipeDTO equipeDto);
        Task Delete(Guid id);
        Task<IEnumerable<PersonagemDTO>> GetPersonagens(Guid equipeId);
    }

    public class EquipeService : IEquipeService
    {
        private readonly IEquipeRepository _equipeRepository;
        private readonly IPersonagemRepository _personagemRepository;
        private readonly IMapper _mapper;

        public EquipeService(
            IEquipeRepository equipeRepository,
            IPersonagemRepository personagemRepository,
            IMapper mapper)
        {
            _equipeRepository = equipeRepository;
            _personagemRepository = personagemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EquipeDTO>> GetAll()
        {
            var equipes = await _equipeRepository.GetAll();
            return _mapper.Map<IEnumerable<EquipeDTO>>(equipes);
        }

        public async Task<EquipeDTO?> GetById(Guid id)
        {
            var equipe = await _equipeRepository.GetById(id);
            return equipe == null ? null : _mapper.Map<EquipeDTO>(equipe);
        }

        public async Task<EquipeDTO> Add(CreateEquipeDTO equipeDto)
        {
            var equipe = _mapper.Map<Equipe>(equipeDto);
            await AdicionarPersonagens(equipe, equipeDto.PersonagensIds);
            await _equipeRepository.Add(equipe);
            return _mapper.Map<EquipeDTO>(equipe);
        }

        public async Task Update(Guid id, UpdateEquipeDTO equipeDto)
        {
            var existingEquipe = await _equipeRepository.GetById(id);
            if (existingEquipe == null)
            {
                throw new KeyNotFoundException("Equipe não encontrada");
            }

            _mapper.Map(equipeDto, existingEquipe);
            await AtualizarPersonagens(existingEquipe, equipeDto.PersonagensIds);
            await _equipeRepository.Update(existingEquipe);
        }

        public async Task Delete(Guid id)
        {
            var equipe = await _equipeRepository.GetById(id);
            if (equipe == null)
            {
                throw new KeyNotFoundException("Equipe não encontrada");
            }

            await _equipeRepository.Delete(equipe);
        }

        public async Task<IEnumerable<PersonagemDTO>> GetPersonagens(Guid equipeId)
        {
            var personagens = await _equipeRepository.GetPersonagensByEquipeId(equipeId);
            return _mapper.Map<IEnumerable<PersonagemDTO>>(personagens);
        }

        private async Task AdicionarPersonagens(Equipe equipe, ICollection<Guid> personagensIds)
        {
            foreach (var personagemId in personagensIds)
            {
                if (await _personagemRepository.Exists(personagemId))
                {
                    equipe.Personagens.Add(new PersonagemEquipe(personagemId, equipe.Id));
                }
            }
        }

        private async Task AtualizarPersonagens(Equipe equipe, ICollection<Guid> personagensIds)
        {
            equipe.Personagens.Clear();
            await AdicionarPersonagens(equipe, personagensIds);
        }
    }
}