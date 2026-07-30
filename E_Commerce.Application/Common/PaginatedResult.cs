using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public sealed class PaginatedResult<TEntity>
    {
        public PaginatedResult(int pageSize, int pageOndex, int count, IReadOnlyList<TEntity> data)
        {
            PageSize = pageSize;
            PageOndex = pageOndex;
            Count = count;
            Data = data;
        }

        public int PageSize { get;  }
        public int PageOndex { get; }
        public int Count { get;  }
        public IReadOnlyList<TEntity> Data { get; }
    }
}
