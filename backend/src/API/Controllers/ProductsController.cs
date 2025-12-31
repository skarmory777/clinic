using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs.Product;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll(
            [FromQuery] bool includeInactive = false)
        {
            var products = await _productService.GetActiveProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("sku/{sku}")]
        public async Task<ActionResult<ProductDto>> GetBySku(string sku)
        {
            var product = await _productService.GetBySkuAsync(sku);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] ProductDto createProductDto)
        {
            var createProduct = await _productService.AddAsync(createProductDto);
            return CreatedAtAction(nameof(GetById), new { id = createProduct.Id }, createProduct);  
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> Update(
            Guid id, 
            [FromBody] ProductDto updateProductDto)
        {
            await _productService.UpdateAsync(updateProductDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.DeleteAsync(new ProductDto { Id = id });
            return NoContent();
        }

        [HttpPatch("{id}/stock")]
        public async Task<ActionResult<ProductDto>> UpdateStock(
            Guid id, 
            [FromBody] UpdateStockDto updateStockDto)
        {
            await _productService.UpdateStockAsync(id, updateStockDto.Quantity);
            return Ok();
        }

        [HttpPatch("{id}/activate")]
        public async Task<ActionResult<ProductDto>> Activate(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();  
            }
            return Ok();
        }

        [HttpPatch("{id}/deactivate")]
        public async Task<ActionResult<ProductDto>> Deactivate(Guid id)
        {
            //var command = new DeactivateProductCommand { Id = id };
            //var result = await _mediator.Send(command);
            //return Ok(result);
            return Ok();
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetLowStock()
        {
            //var query = new GetLowStockProductsQuery();
            //var result = await _mediator.Send(query);
            //return Ok(result);
            return Ok();
        }

        [HttpGet("out-of-stock")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetOutOfStock()
        {
            //var query = new GetOutOfStockProductsQuery();
            //var result = await _mediator.Send(query);
            //return Ok(result);
            return Ok();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Search(
            [FromQuery] string term,
            [FromQuery] bool includeInactive = false)
        {
            var products = await _productService.SearchAsync(term, includeInactive);
            return Ok(products);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<string>>> GetCategories()
        {
            var categories = await _productService.GetAllCategoriesAsync();
            return Ok(categories);    
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByCategory(string category)
        {
            var products = await _productService.GetByCategoryAsync(category);
            return Ok(products);
        }
    }
}