namespace Stock;

using Microsoft.AspNetCore.Mvc;
using Stock.Models;
using Stock.Repository;


[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private IProductRepository _repository;
    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _repository.Get());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        return Created("", await _repository.Add(product));
    }
}