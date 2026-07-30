using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Specifications
{
    internal class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity , Tkey> (IQueryable<TEntity> inputQuery , ISpecifications<TEntity , Tkey> spec) where TEntity : BaseEntity<Tkey>
        {
            var query = inputQuery;

            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if(spec.IncludeExpressions.Any())
            {
                query = spec.IncludeExpressions.Aggregate(query, (current, nextExpression) => current.Include(nextExpression));

            }

            if(spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescinding != null)
            {
                query = query.OrderByDescending(spec.OrderByDescinding);
            }

            if (spec.IsPaginated)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
                return query;
        }
    }
}
