using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        //Geral
         void Add<T>(T entity)where T:class;
         void Update<T>(T entity)where T:class;
         void Delete<T>(T entity)where T:class;

         Task<bool> SaveChengesAsync();

         //EVENTOS
         Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes);
         Task<Evento[]> GetAllEventoAsync(bool includePalestrantes);
         Task<Evento> GetAllEventoAsyncById(int id, bool includePalestrantes);

         //PALESTRANTES
         Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name, bool includeEventos);         
         Task<Palestrante> GetPalestrantesAsyncById(int id, bool includeEventos);
    }
}