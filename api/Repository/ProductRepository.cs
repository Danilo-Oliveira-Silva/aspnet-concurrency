namespace Stock.Repository;

using Stock.Models;
using Stock.Context;
using MongoDB.Driver;
using MongoDB.Bson;

public class ProductRepository : IProductRepository
{
    private IMongoDatabase _database;
    public ProductRepository(IContextConnection contextConnection)
    {
        _database = contextConnection.GetDatabase();
    }

    public async Task<IEnumerable<Product>> Get()
    {
        var filter = Builders<Product>.Filter.Empty;
        var products = await _database.GetCollection<Product>("products").Find(filter).ToListAsync();
        return products;
    }
    public async Task<Product> Add(Product product)
    {
        await _database.GetCollection<Product>("products").InsertOneAsync(product);
        return product;
    }

    public async Task RemoveFromStock(string Guid, int Quantity)
    {
        var id = new ObjectId(Guid);
        var filter = new BsonDocument { { "_id", id } };
        var update = Builders<Product>.Update.Set(p => p.Stock, 11);
        var product = await _database.GetCollection<Product>("products").UpdateOneAsync(filter, update);
    }
}