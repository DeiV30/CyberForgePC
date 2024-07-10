namespace  cyberforgepc.Domain.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Add(List<T> entity);
        void Update(T entity);
        void Remove(T entity);
        Task<T> Find(Expression<Func<T, bool>> predicate);
        Task<T> FindWhere(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);
    }
}
