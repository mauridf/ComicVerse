using ComicVerse.Core.Entities;
using ComicVerse.Core.Interfaces.Repositories;
using ComicVerse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComicVerse.Infrastructure.Repositories
{
    public class EdicaoRepository : IEdicaoRepository
    {
        private readonly ComicVerseDbContext _context;

        public EdicaoRepository(ComicVerseDbContext context)
        {
            _context = context;
        }

        public async Task<Edicao?> GetById(Guid id)
        {
            return await _context.Edicoes
                .Include(e => e.HQ)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Edicao>> GetByHQId(Guid hqId)
        {
            return await _context.Edicoes
                .Where(e => e.HQId == hqId)
                .OrderBy(e => e.Numero)
                .ToListAsync();
        }

        public async Task Add(Edicao edicao)
        {
            await _context.Edicoes.AddAsync(edicao);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Edicao edicao)
        {
            _context.Entry(edicao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Edicao edicao)
        {
            _context.Edicoes.Remove(edicao);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Edicoes.AnyAsync(e => e.Id == id);
        }

        public async Task MarcarComoLida(Guid id, bool lida)
        {
            var edicao = await _context.Edicoes.FindAsync(id);
            if (edicao != null)
            {
                edicao.SetLidaInternal(lida);
                await _context.SaveChangesAsync();
            }
        }
    }
}