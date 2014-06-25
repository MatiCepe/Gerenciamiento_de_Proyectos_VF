using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Windows.Forms;
using GP.AccesoaDatos;
using GP.Dominio;
using GP.Log;
using GP.Repositorio.Interfaces;

namespace GP.Repositorio.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> 
        where T : BaseModel
    {
        private GerenciamientoProyectosContext _gerenciamientoProyectosContext;

        public virtual GerenciamientoProyectosContext GerenciamientoProyectosContext
        {
            get
            {
                try
                {
                    return _gerenciamientoProyectosContext ??
                           (_gerenciamientoProyectosContext = new GerenciamientoProyectosContext());
                }
                catch (Exception e)
                    {
                        var log = new Logger();
                        log.WriteLog(e.ToString());
                        throw new Exception(e.Message, e);
                    }  
            }
        }

        public List<T> GetAll()
        {
            try
            {
                return (List<T>)GerenciamientoProyectosContext.Set<T>().ToList();
            }
            catch (Exception e)
            {
               var log = new Logger();
                log.WriteLog(e.ToString());
                throw new Exception(e.Message, e);
            }  
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return GerenciamientoProyectosContext.Set<T>().FirstOrDefault(predicate);
            }
            catch (Exception e)
            {
                var log = new Logger();
                log.WriteLog(e.ToString());
                throw new Exception(e.Message, e);
            }
        }

        public void Create(T entity)
        {
            try
            {
                GerenciamientoProyectosContext.Set<T>().Add(entity);
                GerenciamientoProyectosContext.SaveChanges();
            }
            catch (Exception e)
            {
                var log = new Logger();
                log.WriteLog(e.ToString());
                throw new Exception(e.Message, e);
            }
        }

        public void Update(T entity)
        {
            try
            {
                GerenciamientoProyectosContext.Entry(entity).State = EntityState.Modified;
                GerenciamientoProyectosContext.SaveChanges();
            }
            catch (Exception e)
            {
                var log = new Logger();
                log.WriteLog(e.ToString());
                throw new Exception(e.Message, e);
            }
        }

        public void Delete(T entity)
        {
            try
            {
                GerenciamientoProyectosContext.Entry(entity).State = EntityState.Deleted;
                GerenciamientoProyectosContext.SaveChanges();
            }
            catch (Exception e)
            {
                var log = new Logger();
                log.WriteLog(e.ToString());
                throw new Exception(e.Message, e);
            }
        }

        public void Dispose()
        {
            if (_gerenciamientoProyectosContext != null)
            {
                _gerenciamientoProyectosContext.Dispose();
                _gerenciamientoProyectosContext = null;
            }
        }

    }
}
