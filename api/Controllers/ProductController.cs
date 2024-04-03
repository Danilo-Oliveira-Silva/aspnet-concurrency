namespace Stock;

using Microsoft.AspNetCore.Mvc;
using Stock.Exceptions;
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
        int a = 0;
        int num = 2 / a;
        return Ok(await _repository.Get());
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        return Created("", await _repository.Add(product));
    }
}