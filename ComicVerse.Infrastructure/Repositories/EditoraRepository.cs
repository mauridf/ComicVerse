using ComicVerse.Core.Entities;
using ComicVerse.Core.Interfaces.Repositories;
using ComicVerse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComicVerse.Infrastructure.Repositories
{
    public class EditoraRepository : IEditoraRepository
    {
        private readonly ComicVerseDbContext _context;

        public EditoraRepository(ComicVerseDbContext context)
        {
            _context = context;
        }

        public async Task<Editora?> GetById(Guid id)
        {
            return await _context.Editoras.FindAsync(id);
        }

        public async Task<IEnumerable<Editora>> GetAll()
        {
            return await _context.Editoras.ToListAsync();
        }

        public async Task Add(Editora editora)
        {
            await _context.Editoras.AddAsync(editora);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Editora editora)
        {
            _context.Entry(editora).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Editora editora)
        {
            _context.Editoras.Remove(editora);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Editoras.AnyAsync(e => e.Id == id);
        }
    }
}