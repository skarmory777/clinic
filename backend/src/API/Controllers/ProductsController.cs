using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs.Product;
//using Application.Features.Products.Commands;
//using Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll(
            [FromQuery] bool includeInactive = false)
        {
            //var query = new GetAllProductsQuery { IncludeInactive = includeInactive };
            //var result = await _mediator.Send(query);
            //return Ok(result);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            //var query = new GetProductByIdQuery { Id = id };
            //var result = await _mediator.Send(query);
            //return Ok(result);
            return Ok();
        }

        [HttpGet("sku/{sku}")]
        public async Task<ActionResult<ProductDto>> GetBySku(string sku)
        {
            //var query = new GetProductBySkuQuery { Sku = sku };
            //var result = await _mediator.Send(query);
            //return Ok(result);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] ProductDto createProductDto)
        {
            //var command = new CreateProductCommand
            //{
            //    Sku = createProductDto.Sku,
            //    Name = createProductDto.Name,
            //    Description = createProductDto.Description,
            //    Category = createProductDto.Category,
            //    UnitPrice = createProductDto.UnitPrice,
            //    CostPrice = createProductDto.CostPrice,
            //    TaxRate = createProductDto.TaxRate,
            //    UnitOfMeasure = createProductDto.UnitOfMeasure,
            //    StockQuantity = createProductDto.StockQuantity,
            //    MinStockLevel = createProductDto.MinStockLevel,
            //    ReorderQuantity = createProductDto.ReorderQuantity,
            //    Notes = createProductDto.Notes
            //};
//
            //var result = await _mediator.Send(command);
            //return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> Update(
            Guid id, 
            [FromBody] ProductDto updateProductDto)
        {
            //var command = new UpdateProductCommand
            //{
            //    Id = id,
            //    Name = updateProductDto.Name,
            //    Description = updateProductDto.Description,
            //    Category = updateProductDto.Category,
            //    UnitPrice = updateProductDto.UnitPrice,
            //    CostPrice = updateProductDto.CostPrice,
            //    TaxRate = updateProductDto.TaxRate,
            //    UnitOfMeasure = updateProductDto.UnitOfMeasure,
            //    MinStockLevel = updateProductDto.MinStockLevel,
            //    ReorderQuantity = updateProductDto.ReorderQuantity,
            //    Notes = updateProductDto.Notes
            //};
//
            //var result = await _mediator.Send(command);
            //return Ok(result);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var command = new DeleteProductCommand { Id = id };
            //await _mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("{id}/stock")]
        public async Task<ActionResult<ProductDto>> UpdateStock(
            Guid id, 
            [FromBody] UpdateStockDto updateStockDto)
        {
            //var command = new UpdateStockCommand
            //{
            //    ProductId = id,
            //    Quantity = updateStockDto.Quantity,
            //    Operation = updateStockDto.Operation
            //};
//
            //var result = await _mediator.Send(command);
            //return Ok(result);
            return Ok();
        }

        [HttpPatch("{id}/activate")]
        public async Task<ActionResult<ProductDto>> Activate(Guid id)
        {
            //var command = new ActivateProductCommand { Id = id };
            //var result = await _mediator.Send(command);
            //return Ok(result);
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
            //var query = new SearchProductsQuery 
            //{ 
            //    SearchTerm = term,
            //    IncludeInactive = includeInactive 
            //};
            //var result = await _mediator.Send(query);
            //return Ok(result);
            return Ok();
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<string>>> GetCategories()
        {
            //var query = new GetProductCategoriesQuery();
            //var result = await _mediator.Send(query);
            //return Ok(result);
            return Ok();
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByCategory(string category)
        {
            //var query = new GetProductsByCategoryQuery { Category = category };
            //var result = await _mediator.Send(query);
            //return Ok(result);
            return Ok();
        }
    }
}