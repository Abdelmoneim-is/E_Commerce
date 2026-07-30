using E_Commerce.Domain.Entities.Baskets;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null, CancellationToken ct = default)
        {
            var value = JsonSerializer.Serialize(basket);
            var result = await _database.StringSetAsync(basket.Id, value, timeToLive ?? TimeSpan.FromDays(7));
            return result ? basket : null;
        }

        public async Task<bool> DeleteBasketAsync(string id, CancellationToken ct = default)
        {
            var result = await _database.KeyDeleteAsync(id);
            return result;
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id, CancellationToken ct = default)
        {
            var result = await _database.StringGetAsync(id);
            return result.IsNullOrEmpty ? null :  JsonSerializer.Deserialize<CustomerBasket>(result!);
        }
    }
}
