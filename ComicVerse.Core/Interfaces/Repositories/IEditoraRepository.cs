using ComicVerse.Core.Entities;

namespace ComicVerse.Core.Interfaces.Repositories
{
    public interface IEditoraRepository
    {
        Task<Editora?> GetById(Guid id);
        Task<IEnumerable<Editora>> GetAll();
        Task Add(Editora editora);
        Task Update(Editora editora);
        Task Delete(Editora editora);
        Task<bool> Exists(Guid id);
    }
}