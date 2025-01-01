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
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext dbContext;

        public ProductRepository(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
        }

        public void Delete(Product product)
        {
            dbContext.Products.Remove(product);
        }

        public async Task<IEnumerable<Product>> GetActiveAsync(IEnumerable<string> filterCategories = null)
        {
            var now = DateTime.Now;
            if (filterCategories is null)
            {
                return await dbContext.Products.Where(p => !p.IsDeleted && p.EndDate > DateTime.Now).ToListAsync();

            }
            else
            {
                return await dbContext.Products.Where(p => !p.IsDeleted && p.EndDate > DateTime.Now && filterCategories.Contains(p.Category.Name)).ToListAsync();
            }
            //return await dbContext.Products.Where(p => (p.StartDate.Add(TimeSpan.FromTicks(p.Duration))) > DateTime.Now).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync(IEnumerable<string> filterCategories = null)
        {
            if (filterCategories is null)
            {
                return await dbContext.Products.Where(p => !p.IsDeleted).Include(p => p.Category).ToListAsync();
            }
            else
            {
                return await dbContext.Products.Where(p => !p.IsDeleted && filterCategories.Contains(p.Category.Name)).Include(p => p.Category).ToListAsync();
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await dbContext.Products.Where(p => p.Id == id).Include(p => p.Category).FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Update(Product product)
        {
            dbContext.Products.Update(product);
        }
    }
}
