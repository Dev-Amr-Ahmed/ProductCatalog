﻿using ProductCatalog.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DAL.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(IEnumerable<string> filterCategories = null);
        Task<IEnumerable<Product>> GetActiveAsync(IEnumerable<string> filterCategories = null);
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task SaveChangesAsync();
        void Update(Product product);
        void Delete(Product product);

    }
}
