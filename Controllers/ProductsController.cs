using OnlineShop.DTOs;
using OnlineShop.Models;
using OnlineShop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductsRepository _products;

    public ProductsController(ILogger<ProductsController> logger,
    IProductsRepository products)
    {
        _logger = logger;
        _products = products;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductsDTO>>> GetAllProducts()
    {
        var productsList = await _products.GetList();

        // User -> UserDTO
        var dtoList = productsList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{product_id}")]
    public async Task<ActionResult<ProductsDTO>> GetById([FromRoute] long product_id)
    {
        var products = await _products.GetById(product_id);

        if (products is null)
            return NotFound("No Product found with given product id");
        return Ok(products.asDto);
    }

    [HttpPost]
    public async Task<ActionResult<ProductsDTO>> CreateProducts([FromBody] ProductsCreateDTO Data)
    {

        var toCreateProducts = new Products
        {
            ProductName = Data.ProductName.Trim(),
            Description = Data.Description.Trim(),
            Price = Data.Price,
            InStock= Data.InStock.Trim()
        };

        var createdProducts = await _products.Create(toCreateProducts);

        return StatusCode(StatusCodes.Status201Created, createdProducts.asDto);
    }

    [HttpPut("{product_id}")]
    public async Task<ActionResult> UpdateProducts([FromRoute] long product_id,
    [FromBody] ProductsUpdateDTO Data)
    {
        var existing = await _products.GetById(product_id);
        if (existing is null)
          return NotFound("No Products found with given product id");

        var toUpdateProducts = existing with
        {
            ProductName = Data.ProductName?.Trim()?.ToLower() ?? existing.ProductName,
            Description = existing.Description,
            Price = existing.Price,
    
        };

        var didUpdate = await _products.Update(toUpdateProducts);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update products");

        return NoContent();
    }

    [HttpDelete("{product_id}")]
    public async Task<ActionResult> DeleteProducts([FromRoute] long product_id)
    {
        var existing = await _products.GetById(product_id);
        if (existing is null)
            return NotFound("No product found with given product id");

        var didDelete = await _products.Delete(product_id);

        return NoContent();
    }
}