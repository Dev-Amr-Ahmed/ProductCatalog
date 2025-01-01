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

        public async Task<IEnumerable<Product>> GetActiveAsync(int filterCategory = 0)
        {
            var now = DateTime.Now;
            if (filterCategory == 0)
            {
                return await dbContext.Products.Where(p => !p.IsDeleted && p.EndDate > DateTime.Now).ToListAsync();

            }
            else
            {
                return await dbContext.Products.Where(p => !p.IsDeleted && p.EndDate > DateTime.Now && p.Category.Id == filterCategory).ToListAsync();
            }
            //return await dbContext.Products.Where(p => (p.StartDate.Add(TimeSpan.FromTicks(p.Duration))) > DateTime.Now).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync(int filterCategory = 0)
        {
            if (filterCategory == 0)
            {
                return await dbContext.Products.Where(p => !p.IsDeleted).Include(p => p.Category).ToListAsync();
            }
            else
            {
                return await dbContext.Products.Where(p => !p.IsDeleted && p.Category.Id == filterCategory).Include(p => p.Category).ToListAsync();
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
