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

        var product = await _database.GetCollection<Product>("products").Find(filter).SingleAsync();
        
        if (product.Stock < Quantity) throw new Exception("Stock insufficient");

        int newStock = product.Stock - Quantity;
        var update = Builders<Product>.Update.Set(p => p.Stock, newStock);
        await _database.GetCollection<Product>("products").UpdateOneAsync(filter, update);
    }

    public async Task AddIntoStock(string Guid, int Quantity)
    {
        //Console.WriteLine("repository - 1 - added to stock");
        var id = new ObjectId(Guid);
        var filter = new BsonDocument { { "_id", id } };

        var product = await _database.GetCollection<Product>("products").Find(filter).SingleAsync();
        //Console.WriteLine("repository - 2 - added to stock - quant: " + product.Stock);

        //int newStock = product.Stock + Quantity;
        //var update = Builders<Product>.Update.Set(p => p.Stock, newStock);
        var update = Builders<Product>.Update.Inc(p => p.Stock, Quantity);
        
        await _database.GetCollection<Product>("products").UpdateOneAsync(filter, update);
        //Console.WriteLine("repository - 3 - added to stock");
    }
}