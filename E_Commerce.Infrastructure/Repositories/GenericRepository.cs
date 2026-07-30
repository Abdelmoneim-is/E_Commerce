using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Infrastructure.Specifications;


namespace E_Commerce.Infrastructure.Repositories
{
    internal class GenericRepository<TEntity, Tkey>(StoreDbContext dbContext) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext _dbContext = dbContext;

        public void Add(TEntity entity) => _dbContext.Add(entity);

        public async Task<int> CountElementAsync(ISpecifications<TEntity, Tkey> spec, CancellationToken ct = default)
        {
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>() , spec).CountAsync(ct);
        }

        public void Delete(TEntity entity) => _dbContext.Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default) =>
            await _dbContext.Set<TEntity>().ToListAsync(ct);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> spec, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), spec);
            return await query.ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(Tkey id, CancellationToken ct = default)
            => await _dbContext.Set<TEntity>().FindAsync(id, ct);

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> spec, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), spec);
            return await query.FirstOrDefaultAsync();
        }

        public void Update(TEntity entity) => _dbContext.Update(entity);
    }
}
