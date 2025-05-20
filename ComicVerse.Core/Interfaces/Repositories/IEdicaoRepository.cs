using ComicVerse.Core.Entities;

namespace ComicVerse.Core.Interfaces.Repositories
{
    public interface IEdicaoRepository
    {
        Task<Edicao?> GetById(Guid id);
        Task<IEnumerable<Edicao>> GetByHQId(Guid hqId);
        Task Add(Edicao edicao);
        Task Update(Edicao edicao);
        Task Delete(Edicao edicao);
        Task<bool> Exists(Guid id);
        Task MarcarComoLida(Guid id, bool lida);
    }
}