namespace Stock.Context;
using MongoDB.Driver;

public class ContextConnection : IContextConnection
{

    private MongoClient _client = null!;
    private string ConnectionString = "";
    private string DatabaseName = "";

    public ContextConnection()
    {
        Connect();   
    }

    public IMongoDatabase GetDatabase() {

        return _client.GetDatabase(DatabaseName);
    }

    private void Connect()
    {
        ConnectionString = "mongodb://root:MyPassword123!@localhost:27017";
        DatabaseName = "Stock";
        //ConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION")!;
        //DatabaseName = Environment.GetEnvironmentVariable("MONGO_DATABASE")!;
        _client = new MongoClient(ConnectionString);
    }
}