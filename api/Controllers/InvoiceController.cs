namespace Stock;

using Microsoft.AspNetCore.Mvc;
using Stock.Repository;

[ApiController]
[Route("[controller]")]
public class InvoiceController : ControllerBase
{
    private IProductRepository _repository;
    public InvoiceController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpPatch("remove/{Product}/{Quant}")]
    public async Task<IActionResult> RemovefromStock(string Product, int Quant)
    {
        try
        {
            await _repository.RemoveFromStock(Product, Quant);
            return Ok();
        }
        catch(Exception err)
        {
            return Conflict(new { message = err.Message.ToString() });
        }
    }

     [HttpPatch("add/{Product}/{Quant}")]
    public async Task<IActionResult> AddIntoStock(string Product, int Quant)
    {
        try
        {
            await _repository.AddIntoStock(Product, Quant);
            //Console.WriteLine("controller - added to stock");
            return Ok();
        }
        catch(Exception err)
        {
            return BadRequest(new { message = err.Message.ToString() });
        }
    }
}