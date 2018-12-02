using ProductApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApp.Models.DTOs;

namespace ProductApp.Models
{
    public static class ModelsUpdater
    {
        public static void UpdateProduct(Product product, ProductForUpdateDto productForUpdateDto)
        {
            product.Name = productForUpdateDto.Name;
            product.Category = productForUpdateDto.Category;
            product.Cost = productForUpdateDto.Cost;
        }
    }
}
