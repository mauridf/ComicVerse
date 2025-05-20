using ComicVerse.Core.Entities;
using ComicVerse.Core.Interfaces.Repositories;
using ComicVerse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComicVerse.Infrastructure.Repositories
{
    public class PersonagemRepository : IPersonagemRepository
    {
        private readonly ComicVerseDbContext _context;

        public PersonagemRepository(ComicVerseDbContext context)
        {
            _context = context;
        }

        public async Task<Personagem?> GetById(Guid id)
        {
            return await _context.Personagens
                .Include(p => p.Equipes)
                .ThenInclude(pe => pe.Equipe)
                .Include(p => p.HQs)
                .ThenInclude(hp => hp.HQ)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Personagem>> GetAll()
        {
            return await _context.Personagens
                .Include(p => p.Equipes)
                .ThenInclude(pe => pe.Equipe)
                .ToListAsync();
        }

        public async Task Add(Personagem personagem)
        {
            await _context.Personagens.AddAsync(personagem);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Personagem personagem)
        {
            _context.Entry(personagem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Personagem personagem)
        {
            _context.Personagens.Remove(personagem);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Personagens.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Personagem>> GetByEquipeId(Guid equipeId)
        {
            return await _context.Personagens
                .Where(p => p.Equipes.Any(pe => pe.EquipeId == equipeId))
                .ToListAsync();
        }
    }
}