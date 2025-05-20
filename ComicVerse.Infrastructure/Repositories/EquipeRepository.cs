using ComicVerse.Core.Entities;
using ComicVerse.Core.Interfaces.Repositories;
using ComicVerse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComicVerse.Infrastructure.Repositories
{
    public class EquipeRepository : IEquipeRepository
    {
        private readonly ComicVerseDbContext _context;

        public EquipeRepository(ComicVerseDbContext context)
        {
            _context = context;
        }

        public async Task<Equipe?> GetById(Guid id)
        {
            return await _context.Equipes
                .Include(e => e.Personagens)
                .ThenInclude(pe => pe.Personagem)
                .Include(e => e.HQs)
                .ThenInclude(he => he.HQ)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Equipe>> GetAll()
        {
            return await _context.Equipes
                .Include(e => e.Personagens)
                .ThenInclude(pe => pe.Personagem)
                .ToListAsync();
        }

        public async Task Add(Equipe equipe)
        {
            await _context.Equipes.AddAsync(equipe);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Equipe equipe)
        {
            _context.Entry(equipe).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Equipe equipe)
        {
            _context.Equipes.Remove(equipe);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Equipes.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Personagem>> GetPersonagensByEquipeId(Guid equipeId)
        {
            return await _context.Personagens
                .Where(p => p.Equipes.Any(pe => pe.EquipeId == equipeId))
                .ToListAsync();
        }
    }
}