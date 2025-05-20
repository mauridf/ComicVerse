using ComicVerse.Core.Entities;

namespace ComicVerse.Core.Interfaces.Repositories
{
    public interface IEquipeRepository
    {
        Task<Equipe?> GetById(Guid id);
        Task<IEnumerable<Equipe>> GetAll();
        Task Add(Equipe equipe);
        Task Update(Equipe equipe);
        Task Delete(Equipe equipe);
        Task<bool> Exists(Guid id);
        Task<IEnumerable<Personagem>> GetPersonagensByEquipeId(Guid equipeId);
    }
}