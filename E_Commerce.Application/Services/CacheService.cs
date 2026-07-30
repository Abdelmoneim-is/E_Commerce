using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class CacheService : ICachService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<string?> GetDataAsync(string cachKey, CancellationToken ct = default)
        {
            return await _cacheRepository.GetAsync(cachKey, ct);
        }

        public async Task SetAsync(string cachKey, object value, TimeSpan? timeToLeave = null, CancellationToken ct = default)
        {
            var jsonValue = JsonSerializer.Serialize(value, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await _cacheRepository.SetAsync(cachKey, jsonValue, timeToLeave, ct);
        }
    }
}
