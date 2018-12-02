using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using ProductApp.Models;
using ProductApp.Models.DTOs;
using ProductApp.Services;

namespace ProductApp.Controllers
{
    [Route("api/products")]
    public class ProductsController: Controller
    {
        private IProductRepository repo;
        private ILogger<ProductsController> logger;

        public ProductsController(IProductRepository repo, ILogger<ProductsController> logger)
        {
            this.repo = repo;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = repo.GetProducts();
            logger.LogInformation("GET on products table has been performed");
            var result = products.Select(ModelsMapper.MapToProductDto);
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public IActionResult GetProduct(int productId)
        {
            var product = repo.GetProduct(productId);
            if (product == null) return NotFound();
            return Ok(ModelsMapper.MapToProductDto(product));
        }

        [HttpPost]
        public IActionResult PostProduct([FromBody] ProductForCreationDto productDto)
        {
            if (productDto == null) return BadRequest();
            if (!ModelState.IsValid)
            {
                LogModelStateErrors();
                return BadRequest(ModelState);
            }

            repo.AddProduct(ModelsMapper.MapToProduct(productDto));
            logger.LogInformation("Added new record to table Products");
            return NoContent();
        }

        [HttpPut("{productId}")]
        public IActionResult PutProduct(int productId, [FromBody] ProductForUpdateDto productDto)
        {
            if (productDto == null) return BadRequest();
            if (!ModelState.IsValid)
            {
                LogModelStateErrors();
                return BadRequest(ModelState);
            }

            var product = repo.GetProduct(productId);
            if (product == null) return NotFound();
            ModelsUpdater.UpdateProduct(product, productDto);

            repo.SaveChanges();
            logger.LogInformation($"Replaced record with id {product.Id} in table Products");
            return Ok(ModelsMapper.MapToProductDto(product));
        }

        private void LogModelStateErrors()
        {
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var modelError in allErrors)
            {
                logger.LogError(modelError.ErrorMessage);
            }
        }
    }
}
