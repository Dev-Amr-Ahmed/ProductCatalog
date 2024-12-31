using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProductCatalog.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DAL.Data
{
    public static class ProductDbSeed
    {
        public static async Task SeedAsync(ProductDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(!dbContext.Categories.Any())
            {
                var categories = new List<Category>(){
                    new Category()
                    {
                        Name = "Category 1"
                    },
                    new Category()
                    {
                        Name = "Category 2"
                    },
                    new Category()
                    {
                        Name = "Category 3"
                    },
                };

                await dbContext.Categories.AddRangeAsync(categories);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var role = new RoleStore<IdentityRole>(dbContext);
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!dbContext.Users.Any())
            {
                var user = new IdentityUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");

                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
