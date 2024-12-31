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
        public static async Task SeedAsync(ProductDbContext dbContext)
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
        }
    }
}
