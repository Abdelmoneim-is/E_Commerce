using E_Commerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    internal class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _dataBase;
        public CacheRepository(IConnectionMultiplexer connection)
        {
            _dataBase = connection.GetDatabase();
        }

        public async Task<string?> GetAsync(string value, CancellationToken ct = default)
        {
            var result = await _dataBase.StringGetAsync(value);
            if(result.IsNullOrEmpty)
            {
                return null;
            }
            else
            {
                return result.ToString();
            }
        }

        public async Task SetAsync(string keyCashe, string valueCache, TimeSpan? timeToLeave = null, CancellationToken ct = default)
        {
            await _dataBase.StringSetAsync(keyCashe, valueCache, timeToLeave ?? TimeSpan.FromDays(2));
        }
    }
}
