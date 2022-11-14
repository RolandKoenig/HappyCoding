using HappyCoding.AspNetCoreSwaggerGen.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HappyCoding.AspNetCoreSwaggerGen.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ILogger<ProductsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = nameof(GetProductAsync))]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<IActionResult> GetProductAsync(Guid id)
    {
        await Task.Delay(100);

        return Ok(new ProductDto()
        {
            ID = id,
            Name = "Test Product",
            Description = "This is a randomly generated product"
        });
    }

    [HttpPut(Name = nameof(CreateProductAsync))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<IActionResult> CreateProductAsync(ProductDto product)
    {
        await Task.Delay(100);
        
        var createdProduct = product with {ID = Guid.NewGuid()};
        return Created(createdProduct.ID.ToString(), createdProduct);
    }

    [HttpGet(template: "{productType}", Name = nameof(GetProductsByProductTypeAsync))]
    [ProducesResponseType(typeof(IReadOnlyList<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<IActionResult> GetProductsByProductTypeAsync(ProductType productType)
    {
        await Task.Delay(100);
        
        return Ok(new[]{
            new ProductDto()
            {
                ID = Guid.NewGuid(),
                Name = "Test Product",
                Description = "This is a randomly generated product",
                Type = productType
            },
            new ProductDto()
            {
                ID = Guid.NewGuid(),
                Name = "Test Product",
                Description = "This is a randomly generated product",
                Type = productType
            }
        });
    }

}