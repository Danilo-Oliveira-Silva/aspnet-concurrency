namespace Stock.Repository;

using Stock.Models;

public interface IProductRepository
{
    Task<IEnumerable<Product>> Get();
    Task<Product> Add(Product product);
    Task RemoveFromStock(string Guid, int Quantity);
}