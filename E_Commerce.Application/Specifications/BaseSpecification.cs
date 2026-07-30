using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal abstract class BaseSpecification<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
    

        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity, bool>> Criteria { get; private set; }

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDescinding { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void ApplyPagination (int pageSize , int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }

        protected void AddOrderBy (Expression<Func<TEntity , object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescinding (Expression<Func<TEntity , object>> orderByDescindingExpression)
        {
            OrderByDescinding = orderByDescindingExpression;
        }

        protected BaseSpecification(Expression<Func<TEntity, bool>> createria)
        {
            this.Criteria = createria;
        }

        protected void AddInclude (Expression<Func<TEntity , object>> include)
        {
            IncludeExpressions.Add(include);
        }
    }
}
