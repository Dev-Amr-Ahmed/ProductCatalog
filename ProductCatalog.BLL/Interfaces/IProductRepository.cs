using ProductCatalog.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DAL.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(int filterCategory = 0);
        Task<IEnumerable<Product>> GetActiveAsync(int filterCategory = 0);
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task SaveChangesAsync();
        void Update(Product product);
        void Delete(Product product);

    }
}
