using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface ICachService
    {
        Task<string?> GetDataAsync(string cachKey, CancellationToken ct = default);
        Task SetAsync(string cachKey, object value, TimeSpan? timeToLeave = default, CancellationToken ct = default);
    }
}
