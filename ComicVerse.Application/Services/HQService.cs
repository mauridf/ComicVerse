using AutoMapper;
using ComicVerse.Application.DTOs;
using ComicVerse.Core.Entities;
using ComicVerse.Core.Interfaces.Repositories;

namespace ComicVerse.Application.Services
{
    public interface IHQService
    {
        Task<IEnumerable<HQDTO>> GetAll();
        Task<HQDTO?> GetById(Guid id);
        Task<HQDTO> Add(CreateHQDTO hqDto);
        Task Update(Guid id, UpdateHQDTO hqDto);
        Task Delete(Guid id);
        Task AdicionarEdicao(Guid hqId, CreateEdicaoDTO edicaoDto);
    }

    public class HQService : IHQService
    {
        private readonly IHQRepository _hqRepository;
        private readonly IEditoraRepository _editoraRepository;
        private readonly IPersonagemRepository _personagemRepository;
        private readonly IEquipeRepository _equipeRepository;
        private readonly IMapper _mapper;

        public HQService(
            IHQRepository hqRepository,
            IEditoraRepository editoraRepository,
            IPersonagemRepository personagemRepository,
            IEquipeRepository equipeRepository,
            IMapper mapper)
        {
            _hqRepository = hqRepository;
            _editoraRepository = editoraRepository;
            _personagemRepository = personagemRepository;
            _equipeRepository = equipeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HQDTO>> GetAll()
        {
            var hqs = await _hqRepository.GetAll();
            return _mapper.Map<IEnumerable<HQDTO>>(hqs);
        }

        public async Task<HQDTO?> GetById(Guid id)
        {
            var hq = await _hqRepository.GetById(id);
            return hq == null ? null : _mapper.Map<HQDTO>(hq);
        }

        public async Task<HQDTO> Add(CreateHQDTO hqDto)
        {
            var hq = _mapper.Map<HQ>(hqDto);

            // Adicionar relacionamentos
            await AdicionarRelacionamentos(hq, hqDto);

            await _hqRepository.Add(hq);
            return _mapper.Map<HQDTO>(hq);
        }

        public async Task Update(Guid id, UpdateHQDTO hqDto)
        {
            var existingHQ = await _hqRepository.GetById(id);
            if (existingHQ == null)
            {
                throw new KeyNotFoundException("HQ não encontrada");
            }

            _mapper.Map(hqDto, existingHQ);

            // Atualizar relacionamentos
            await AtualizarRelacionamentos(existingHQ, hqDto);

            await _hqRepository.Update(existingHQ);
        }

        public async Task Delete(Guid id)
        {
            var hq = await _hqRepository.GetById(id);
            if (hq == null)
            {
                throw new KeyNotFoundException("HQ não encontrada");
            }

            await _hqRepository.Delete(hq);
        }

        public async Task AdicionarEdicao(Guid hqId, CreateEdicaoDTO edicaoDto)
        {
            var hq = await _hqRepository.GetById(hqId);
            if (hq == null)
            {
                throw new KeyNotFoundException("HQ não encontrada");
            }

            var edicao = new Edicao(
                hqId: hqId,
                numero: edicaoDto.Numero,
                titulo: edicaoDto.Titulo,
                dataLancamento: edicaoDto.DataLancamento,
                imagem: edicaoDto.Imagem,
                observacoes: edicaoDto.Observacoes,
                lida: edicaoDto.Lida,
                nota: edicaoDto.Nota
            );

            hq.Edicoes.Add(edicao);
            await _hqRepository.Update(hq);
        }

        private async Task AdicionarRelacionamentos(HQ hq, CreateHQDTO hqDto)
        {
            // Adicionar editoras
            foreach (var editoraId in hqDto.EditorasIds)
            {
                if (await _editoraRepository.Exists(editoraId))
                {
                    hq.Editoras.Add(new HQEditora(hq.Id, editoraId));
                }
            }

            // Adicionar personagens
            foreach (var personagemId in hqDto.PersonagensIds)
            {
                if (await _personagemRepository.Exists(personagemId))
                {
                    hq.Personagens.Add(new HQPersonagem(hq.Id, personagemId));
                }
            }

            // Adicionar equipes
            foreach (var equipeId in hqDto.EquipesIds)
            {
                if (await _equipeRepository.Exists(equipeId))
                {
                    hq.Equipes.Add(new HQEquipe(hq.Id, equipeId));
                }
            }
        }

        private async Task AtualizarRelacionamentos(HQ hq, UpdateHQDTO hqDto)
        {
            // Limpar relacionamentos existentes
            hq.Editoras.Clear();
            hq.Personagens.Clear();
            hq.Equipes.Clear();

            // Adicionar novos relacionamentos
            await AdicionarRelacionamentos(hq, new CreateHQDTO
            {
                EditorasIds = hqDto.EditorasIds,
                PersonagensIds = hqDto.PersonagensIds,
                EquipesIds = hqDto.EquipesIds
            });
        }
    }
}