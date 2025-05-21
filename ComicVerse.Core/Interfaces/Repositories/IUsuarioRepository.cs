using ComicVerse.Core.Entities;

namespace ComicVerse.Core.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObterPorEmail(string email);
        Task<bool> ExisteUsuario(string email);
        Task Adicionar(Usuario usuario);
        Task<Usuario> ObterPorId(Guid id);
    }
}