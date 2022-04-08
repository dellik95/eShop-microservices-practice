using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CatalogController : ControllerBase
{
	private readonly IProductRepository _repository;
	private readonly ILogger<CatalogController> _logger;

	public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
	{
		_repository = repository;
		_logger = logger;
	}


	[HttpGet(Name = nameof(GetProducts))]
	public async Task<IActionResult> GetProducts()
	{
		var products = await _repository.GetProductsByConditions(null);
		return Ok(products);
	}


	[HttpGet("{id:length(24)}", Name = nameof(GetProduct))]
	public async Task<IActionResult> GetProduct(string id)
	{
		var product = await _repository.GetProductByConditions(p => p.Id == id);
		if (product is null)
			return NotFound();
		return Ok(product);
	}


	[HttpGet("[action]/{category}", Name = nameof(GetProductByCategory))]
	public async Task<IActionResult> GetProductByCategory(string category)
	{
		var products = await _repository.GetProductsByConditions(p => p.Category == category);
		return Ok(products);
	}


	[HttpPost(Name = nameof(CreateProduct))]
	public async Task<IActionResult> CreateProduct([FromBody] Product product)
	{
		await _repository.CreateProduct(product);
		return CreatedAtRoute(nameof(GetProduct), new {Id = product.Id});
	}

	[HttpPut(Name = nameof(UpdateProduct))]
	public async Task<IActionResult> UpdateProduct([FromBody] Product product)
	{
		await _repository.UpdateProduct(product);
		return NoContent();
	}


	[HttpDelete("{id:length(24)}", Name = nameof(DeleteProduct))]
	public async Task<IActionResult> DeleteProduct(string id)
	{
		await _repository.DeleteProduct(id);
		return NoContent();
	}
}