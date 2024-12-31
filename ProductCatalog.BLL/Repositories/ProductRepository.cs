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

        public Task<IEnumerable<Product>> GetActiveAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await dbContext.Products.Include(p=>p.Category).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await dbContext.Products.Where(p=>p.Id == id).FirstOrDefaultAsync();
        }

        public void Update(Product product)
        {
            dbContext.Products.Update(product);
        }
    }
}
