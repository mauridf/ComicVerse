using ComicVerse.Core.Entities;

namespace ComicVerse.Core.Interfaces.Repositories
{
    public interface IHQRepository
    {
        Task<HQ?> GetById(Guid id);
        Task<IEnumerable<HQ>> GetAll();
        Task Add(HQ hq);
        Task Update(HQ hq);
        Task Delete(HQ hq);
        Task<bool> Exists(Guid id);
        Task AdicionarEdicao(Edicao edicao);
    }
}