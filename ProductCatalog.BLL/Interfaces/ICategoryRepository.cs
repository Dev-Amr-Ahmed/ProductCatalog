using ProductCatalog.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DAL.Data.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task<Category?> FindAsync(int id);
        Task SaveChangesAsync();
        Task<IEnumerable<Category>> GetAllAsync();
        void Delete(Category category);
    }
}
