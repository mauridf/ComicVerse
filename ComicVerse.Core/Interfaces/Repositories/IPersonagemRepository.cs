using ComicVerse.Core.Entities;

namespace ComicVerse.Core.Interfaces.Repositories
{
    public interface IPersonagemRepository
    {
        Task<Personagem?> GetById(Guid id);
        Task<IEnumerable<Personagem>> GetAll();
        Task Add(Personagem personagem);
        Task Update(Personagem personagem);
        Task Delete(Personagem personagem);
        Task<bool> Exists(Guid id);
        Task<IEnumerable<Personagem>> GetByEquipeId(Guid equipeId);
    }
}