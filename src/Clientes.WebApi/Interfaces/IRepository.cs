using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clientes.WebApi.Interfaces
{
    public interface IRepository<T> where T : class 
    {
        void Add(T entity);
        Task<long> CountAsync();
        Task<IList<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIDAsync(long id);
        void Remove(T entity);
        Task SaveChangesAsync();
        void Update(T entity);
    }
}