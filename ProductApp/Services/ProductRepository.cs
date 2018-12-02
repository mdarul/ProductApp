using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApp.Models.Entities;

namespace ProductApp.Services
{
    public class ProductRepository: IProductRepository
    {
        private ProductContext context;

        public ProductRepository(ProductContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return context.products.ToList();
        }

        public Product GetProduct(int productId)
        {
            return context.products.ToList().FirstOrDefault(o => o.Id == productId);
        }

        public void AddProduct(Product product)
        {
            context.products.Add(product);
            SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
