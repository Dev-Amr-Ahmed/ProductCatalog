using Microsoft.EntityFrameworkCore;
using ProductCatalog.DAL.Data.Interfaces;
using ProductCatalog.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DAL.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductDbContext dbContext;

        public CategoryRepository(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
        }
        public void Delete(Category category)
        {
            dbContext.Categories.Remove(category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }
    }
}
