using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GP.Dominio;

namespace GP.Repositorio.Interfaces
{
    interface IRepository<T> 
        where T : BaseModel
    {
        List<T> GetAll();
        T Single(Expression<Func<T, bool>> predicate);
        void Create( T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
