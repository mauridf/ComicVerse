using AutoMapper;
using ComicVerse.Application.DTOs;
using ComicVerse.Core.Entities;
using ComicVerse.Core.Interfaces.Repositories;

namespace ComicVerse.Application.Services
{
    public interface IPersonagemService
    {
        Task<IEnumerable<PersonagemDTO>> GetAll();
        Task<PersonagemDTO?> GetById(Guid id);
        Task<PersonagemDTO> Add(CreatePersonagemDTO personagemDto);
        Task Update(Guid id, UpdatePersonagemDTO personagemDto);
        Task Delete(Guid id);
    }

    public class PersonagemService : IPersonagemService
    {
        private readonly IPersonagemRepository _personagemRepository;
        private readonly IEquipeRepository _equipeRepository;
        private readonly IMapper _mapper;

        public PersonagemService(
            IPersonagemRepository personagemRepository,
            IEquipeRepository equipeRepository,
            IMapper mapper)
        {
            _personagemRepository = personagemRepository;
            _equipeRepository = equipeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonagemDTO>> GetAll()
        {
            var personagens = await _personagemRepository.GetAll();
            return _mapper.Map<IEnumerable<PersonagemDTO>>(personagens);
        }

        public async Task<PersonagemDTO?> GetById(Guid id)
        {
            var personagem = await _personagemRepository.GetById(id);
            return personagem == null ? null : _mapper.Map<PersonagemDTO>(personagem);
        }

        public async Task<PersonagemDTO> Add(CreatePersonagemDTO personagemDto)
        {
            var personagem = _mapper.Map<Personagem>(personagemDto);
            await AdicionarEquipes(personagem, personagemDto.EquipesIds);
            await _personagemRepository.Add(personagem);
            return _mapper.Map<PersonagemDTO>(personagem);
        }

        public async Task Update(Guid id, UpdatePersonagemDTO personagemDto)
        {
            var existingPersonagem = await _personagemRepository.GetById(id);
            if (existingPersonagem == null)
            {
                throw new KeyNotFoundException("Personagem não encontrado");
            }

            _mapper.Map(personagemDto, existingPersonagem);
            await AtualizarEquipes(existingPersonagem, personagemDto.EquipesIds);
            await _personagemRepository.Update(existingPersonagem);
        }

        public async Task Delete(Guid id)
        {
            var personagem = await _personagemRepository.GetById(id);
            if (personagem == null)
            {
                throw new KeyNotFoundException("Personagem não encontrado");
            }

            await _personagemRepository.Delete(personagem);
        }

        private async Task AdicionarEquipes(Personagem personagem, ICollection<Guid> equipesIds)
        {
            foreach (var equipeId in equipesIds)
            {
                if (await _equipeRepository.Exists(equipeId))
                {
                    personagem.Equipes.Add(new PersonagemEquipe(personagem.Id, equipeId));
                }
            }
        }

        private async Task AtualizarEquipes(Personagem personagem, ICollection<Guid> equipesIds)
        {
            personagem.Equipes.Clear();
            await AdicionarEquipes(personagem, equipesIds);
        }
    }
}