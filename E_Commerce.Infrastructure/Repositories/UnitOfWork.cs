using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    internal class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string , object> repositories = [];
        public IGenericRepository<TEntity, TKey> GetGenericRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if(repositories.TryGetValue(typeName , out object? value))
            {
                return (IGenericRepository<TEntity, TKey>)value;
            }
            else
            {
                var repo = new GenericRepository<TEntity, TKey>(dbContext);
                repositories[typeName] = repo;
                return repo;
            }
        }

        public async Task<int> SaveChanges(CancellationToken ct = default)
        {
            return await dbContext.SaveChangesAsync(ct);
        }
    }
}
