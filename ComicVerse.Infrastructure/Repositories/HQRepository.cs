using ComicVerse.Core.Entities;
using ComicVerse.Core.Interfaces.Repositories;
using ComicVerse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComicVerse.Infrastructure.Repositories
{
    public class HQRepository : IHQRepository
    {
        private readonly ComicVerseDbContext _context;

        public HQRepository(ComicVerseDbContext context)
        {
            _context = context;
        }

        public async Task<HQ?> GetById(Guid id)
        {
            return await _context.HQs
                .Include(h => h.Editoras)
                .ThenInclude(he => he.Editora)
                .Include(h => h.Personagens)
                .ThenInclude(hp => hp.Personagem)
                .Include(h => h.Equipes)
                .ThenInclude(he => he.Equipe)
                .Include(h => h.Edicoes)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<HQ>> GetAll()
        {
            return await _context.HQs
                .Include(h => h.Editoras)
                .ThenInclude(he => he.Editora)
                .ToListAsync();
        }

        public async Task Add(HQ hq)
        {
            await _context.HQs.AddAsync(hq);
            await _context.SaveChangesAsync();
        }

        public async Task Update(HQ hq)
        {
            _context.Entry(hq).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(HQ hq)
        {
            _context.HQs.Remove(hq);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.HQs.AnyAsync(h => h.Id == id);
        }

        public async Task AdicionarEdicao(Edicao edicao)
        {
            await _context.Edicoes.AddAsync(edicao);
            await _context.SaveChangesAsync();
        }
    }
}