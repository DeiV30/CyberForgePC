namespace cyberforgepc.Domain.Repository
{
    using cyberforgepc.Database.Context;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Repository<T> : IRepository<T> where T : class
    {
        protected CyberforgepcContext Context;
        protected readonly DbSet<T> dbSet;

        public Repository(CyberforgepcContext context)
        {
            this.Context = context;
            dbSet = context.Set<T>();
        }

        public void Add(T entity) => dbSet.AddAsync(entity);

        public void Add(List<T> entity)
        {
            foreach (var item in entity)
                dbSet.AddAsync(item);
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            dbSet.Update(entity);
        }

        public void Remove(T entity) => dbSet.Remove(entity);

        public async Task<T> Find(Expression<Func<T, bool>> predicate) => await dbSet.FirstOrDefaultAsync(predicate);

        public async Task<T> FindWhere(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            if (includes.Length == 0)
                return await dbSet.Where(predicate).FirstOrDefaultAsync();
            else
            {
                IQueryable<T> query = dbSet;

                foreach (var include in includes)
                    query = query.Include(include).Where(predicate);

                return await query.FirstOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            if (includes.Length != 0)
            {
                foreach (var include in includes)
                    query = query.Include(include);

                return await query.ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            if (includes.Length == 0)
                return await dbSet.Where(predicate).ToListAsync();
            else
            {
                IQueryable<T> query = dbSet;

                foreach (var include in includes)
                    query = query.Include(include).Where(predicate);

                return await query.ToListAsync();
            }
        }

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate) => dbSet.CountAsync(predicate);

        public async Task ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            var sqlParameters = parameters ?? Array.Empty<SqlParameter>();
            var parameterNames = string.Join(", ", sqlParameters.Select(p => p.ParameterName));

            var sqlCommand = $"EXEC {procedureName} {parameterNames}";
            await Context.Database.ExecuteSqlRawAsync(sqlCommand, sqlParameters);
        }
    }
}
