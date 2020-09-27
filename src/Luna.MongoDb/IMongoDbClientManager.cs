using MongoDB.Driver;

namespace Luna.MongoDb
{
    public interface IMongoDbClientManager
    {
        IMongoCollection<T> GetMongoCollection<T>(string database = null, string collection = null);
        IMongoDatabase GetMongoDatabase(string database = null);
        IMongoClient GetMongoClient();
    }
}