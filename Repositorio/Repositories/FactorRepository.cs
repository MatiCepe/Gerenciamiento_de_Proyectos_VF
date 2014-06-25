using System;
using System.Linq;
using GP.Dominio.Models;
using GP.Repositorio.Interfaces;

namespace GP.Repositorio.Repositories
{
    public class FactorRepository : BaseRepository<Factor>, IFactorRepository
    {
        public Factor GetByID(int id)
        {
            try
            {
                return GerenciamientoProyectosContext.Set<Factor>()
               .FirstOrDefault(x => x.FactorId == id);
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message,e);
            }
           
        }

        public Valor GetValorById(int id)
        {
            try
            {
                return GerenciamientoProyectosContext.Valor.Find(id);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
    }
}
