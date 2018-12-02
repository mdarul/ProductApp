using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApp.Models.DTOs;
using ProductApp.Models.Entities;

namespace ProductApp.Models
{
    public static class ModelsMapper
    {
        public static Product MapToProduct(ProductForCreationDto productDto)
        {
            return new Product()
            {
                Name = productDto.Name,
                Cost = productDto.Cost,
                Category = productDto.Category
            };
        }

        public static Product MapToProduct(ProductForUpdateDto productDto)
        {
            return new Product()
            {
                Name = productDto.Name,
                Cost = productDto.Cost,
                Category = productDto.Category
            };
        }

        public static ProductDto MapToProductDto(Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Category = product.Category
            };
        }
    }
}
