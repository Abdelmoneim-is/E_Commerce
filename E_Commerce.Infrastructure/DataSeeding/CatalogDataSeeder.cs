using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.DataSeeding
{
    internal class CatalogDataSeeder (StoreDbContext dbContext , ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
        //D:\Route\API\ProjectECommerce\E_Commerce\E_Commerce.API\bin\Debug\net8.0\DataSeeder\products
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigration = await dbContext.Database.GetPendingMigrationsAsync(ct);

                if (pendingMigration.Any())
                    await dbContext.Database.MigrateAsync(ct);

                var rootPath = Path.Combine(AppContext.BaseDirectory, "DataSeeder");

                logger.LogInformation($"Root Path = {rootPath}");

                // Seed Product Brands
                await IfEmptySeeding<ProductBrand, int>(rootPath, "brands.json", ct);
                await dbContext.SaveChangesAsync(ct);

                // Seed Product Types
                await IfEmptySeeding<ProductType, int>(rootPath, "types.json", ct);
                await dbContext.SaveChangesAsync(ct);

                // Seed Products
                await IfEmptySeeding<Product, int>(rootPath, "products.json", ct);
                var result = await dbContext.SaveChangesAsync(ct);

                if (result > 0)
                {
                    logger.LogInformation($"{result} Rows Added");
                }
                else
                {
                    logger.LogInformation("Database Already Seeded");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private async Task IfEmptySeeding<T, Tkey>(string rootPath, string fileName, CancellationToken ct) where T : BaseEntity<Tkey>
        {
            if (await dbContext.Set<T>().AnyAsync(ct))
            {
                logger.LogInformation("Table Already Has Data");
                return;
            }

            var filePath = Path.Combine(rootPath, fileName);
             
            if (!File.Exists(filePath))
            {
                logger.LogWarning($"File {fileName} Not Exists");
                return;
            }

            using var fileStream = File.OpenRead(filePath);

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var item =await JsonSerializer.DeserializeAsync<List<T>>(fileStream , options , ct);
            if (item?.Any() ?? false)
                dbContext.Set<T>().AddRange(item);

        }
    }
}
