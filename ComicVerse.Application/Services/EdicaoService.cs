using AutoMapper;
using ComicVerse.Application.DTOs;
using ComicVerse.Core.Entities;
using ComicVerse.Core.Interfaces.Repositories;

namespace ComicVerse.Application.Services
{
    public interface IEdicaoService
    {
        Task<IEnumerable<EdicaoDTO>> GetByHQId(Guid hqId);
        Task<EdicaoDTO?> GetById(Guid id);
        Task<EdicaoDTO> Add(Guid hqId, CreateEdicaoDTO edicaoDto);
        Task Update(Guid id, UpdateEdicaoDTO edicaoDto);
        Task Delete(Guid id);
        Task MarcarComoLida(Guid id, bool lida);
    }

    public class EdicaoService : IEdicaoService
    {
        private readonly IEdicaoRepository _edicaoRepository;
        private readonly IHQRepository _hqRepository;
        private readonly IMapper _mapper;

        public EdicaoService(
            IEdicaoRepository edicaoRepository,
            IHQRepository hqRepository,
            IMapper mapper)
        {
            _edicaoRepository = edicaoRepository;
            _hqRepository = hqRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EdicaoDTO>> GetByHQId(Guid hqId)
        {
            var edicoes = await _edicaoRepository.GetByHQId(hqId);
            return _mapper.Map<IEnumerable<EdicaoDTO>>(edicoes);
        }

        public async Task<EdicaoDTO?> GetById(Guid id)
        {
            var edicao = await _edicaoRepository.GetById(id);
            return edicao == null ? null : _mapper.Map<EdicaoDTO>(edicao);
        }

        public async Task<EdicaoDTO> Add(Guid hqId, CreateEdicaoDTO edicaoDto)
        {
            var edicao = _mapper.Map<Edicao>(edicaoDto);
            edicao.HQId = hqId;

            // Garante UTC se não foi tratado no DTO/mapper
            if (edicao.DataLancamento.HasValue && edicao.DataLancamento.Value.Kind == DateTimeKind.Unspecified)
            {
                edicao.DataLancamento = DateTime.SpecifyKind(edicao.DataLancamento.Value, DateTimeKind.Utc);
            }

            await _edicaoRepository.Add(edicao);
            return _mapper.Map<EdicaoDTO>(edicao);
        }

        public async Task Update(Guid id, UpdateEdicaoDTO edicaoDto)
        {
            var existingEdicao = await _edicaoRepository.GetById(id);
            if (existingEdicao == null)
            {
                throw new KeyNotFoundException("Edição não encontrada");
            }

            _mapper.Map(edicaoDto, existingEdicao);
            await _edicaoRepository.Update(existingEdicao);
        }

        public async Task Delete(Guid id)
        {
            var edicao = await _edicaoRepository.GetById(id);
            if (edicao == null)
            {
                throw new KeyNotFoundException("Edição não encontrada");
            }

            await _edicaoRepository.Delete(edicao);
        }

        public async Task MarcarComoLida(Guid id, bool lida)
        {
            var edicao = await _edicaoRepository.GetById(id);
            if (edicao != null)
            {
                edicao.MarcarComoLida(lida);
                await _edicaoRepository.Update(edicao);
            }
        }
    }
}