using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApp.Models.Entities;

namespace ProductApp.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int productId);
        void AddProduct(Product product);
        void SaveChanges();
    }
}
