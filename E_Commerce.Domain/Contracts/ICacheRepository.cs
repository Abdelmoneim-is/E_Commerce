using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string value, CancellationToken ct = default);
        Task SetAsync(string keyCashe, string valueCache, TimeSpan? timeToLeave = default ,CancellationToken ct = default);
    }
}
