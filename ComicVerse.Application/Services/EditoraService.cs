using AutoMapper;
using ComicVerse.Application.DTOs;
using ComicVerse.Core.Entities;
using ComicVerse.Core.Interfaces.Repositories;

namespace ComicVerse.Application.Services
{
    public interface IEditoraService
    {
        Task<IEnumerable<EditoraDTO>> GetAll();
        Task<EditoraDTO?> GetById(Guid id);
        Task<EditoraDTO> Add(CreateEditoraDTO editoraDto);
        Task Update(Guid id, UpdateEditoraDTO editoraDto);
        Task Delete(Guid id);
    }

    public class EditoraService : IEditoraService
    {
        private readonly IEditoraRepository _editoraRepository;
        private readonly IMapper _mapper;

        public EditoraService(IEditoraRepository editoraRepository, IMapper mapper)
        {
            _editoraRepository = editoraRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EditoraDTO>> GetAll()
        {
            var editoras = await _editoraRepository.GetAll();
            return _mapper.Map<IEnumerable<EditoraDTO>>(editoras);
        }

        public async Task<EditoraDTO?> GetById(Guid id)
        {
            var editora = await _editoraRepository.GetById(id);
            return editora == null ? null : _mapper.Map<EditoraDTO>(editora);
        }

        public async Task<EditoraDTO> Add(CreateEditoraDTO editoraDto)
        {
            var editora = _mapper.Map<Editora>(editoraDto);
            await _editoraRepository.Add(editora);
            return _mapper.Map<EditoraDTO>(editora);
        }

        public async Task Update(Guid id, UpdateEditoraDTO editoraDto)
        {
            var existingEditora = await _editoraRepository.GetById(id);
            if (existingEditora == null)
            {
                throw new KeyNotFoundException("Editora não encontrada");
            }

            _mapper.Map(editoraDto, existingEditora);
            await _editoraRepository.Update(existingEditora);
        }

        public async Task Delete(Guid id)
        {
            var editora = await _editoraRepository.GetById(id);
            if (editora == null)
            {
                throw new KeyNotFoundException("Editora não encontrada");
            }

            await _editoraRepository.Delete(editora);
        }
    }
}