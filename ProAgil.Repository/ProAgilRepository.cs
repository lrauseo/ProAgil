using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository.Data;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        public ProAgilContext _context { get; }
        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class => _context.Add(entity);
        public void Update<T>(T entity) where T : class => _context.Update(entity);

        public void Delete<T>(T entity) where T : class => _context.Remove(entity);

        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                                .Include(a => a.Lotes)
                                .Include(b => b.RedesSociais);
            if (includePalestrantes)
            {
                query = query.Include(a => a.PalestranteEventos).ThenInclude(b => b.Palestrante);

            }

            query = query.AsNoTracking().OrderByDescending(o => o.DataEvento);
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                                                .Include(a => a.Lotes)
                                                .Include(b => b.RedesSociais)
                                                .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));
            if (includePalestrantes)
            {
                query = query.Include(a => a.PalestranteEventos)
                            .ThenInclude(b => b.Palestrante);
            }
            query = query.AsNoTracking().OrderByDescending(a => a.DataEvento);
            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetAllEventoAsyncById(int eventId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                                                .Include(a => a.Lotes)
                                                .Include(b => b.RedesSociais)
                                                .Where(a => a.Id == eventId);

            if (includePalestrantes)
            {
                query = query.Include(a => a.PalestranteEventos)
                            .ThenInclude(b => b.Palestrante);
            }
            query = query.AsNoTracking().OrderByDescending(a => a.DataEvento);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                                .Include(b => b.RedesSociais)
                                                .Where(a => a.Nome.ToLower().Contains(name.ToLower()));

            if (includeEventos)
            {
                query = query.Include(a => a.PalestranteEventos)
                            .ThenInclude(b => b.Palestrante);
            }
            query = query.AsNoTracking().OrderBy(a => a.Nome);
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestrantesAsyncById(int id, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                                   .Include(b => b.RedesSociais)
                                                   .Where(a => a.Id == id);

            if (includeEventos)
            {
                query = query.Include(a => a.PalestranteEventos)
                            .ThenInclude(b => b.Palestrante);
            }
            query = query.AsNoTracking().OrderBy(a => a.Nome);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChengesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }        
    }
}