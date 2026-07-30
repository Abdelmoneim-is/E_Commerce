using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Identity.Data;
using E_Commerce.Infrastructure.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.DataSeeding
{
    internal class IdentityDataSeeder : IDataSeeder
    {
        private readonly StoreIdentityDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataSeeder> _logger;

        public IdentityDataSeeder(StoreIdentityDbContext dbContext ,
            UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager ,
            ILogger<IdentityDataSeeder> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }



        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigration = await _dbContext.Database.GetPendingMigrationsAsync(ct);
                if (pendingMigration.Any())
                {
                    await _dbContext.Database.MigrateAsync(ct);
                }
                if (!await _roleManager.Roles.AnyAsync())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!await _userManager.Users.AnyAsync())
                {
                    var admin = new AppUser()
                    {
                        DisplayName = "Abdo Khaled",
                        Email = "abdokhalid705@gmail.com",
                        UserName = "abdokhalid",
                        PhoneNumber = "01032629225"
                    };

                    var createUser = await _userManager.CreateAsync(admin, "P@ssw0rd");
                    if (createUser.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(admin, "SuperAdmin");
                    }
                    else
                    {
                        var errors = string.Join(';', createUser.Errors.Select(x => x.Description));
                        _logger.LogWarning($"Can Not Seed Default Admin{errors}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Identity DataSeeding Failed");
                return;
            }
 
        }
    }
}
