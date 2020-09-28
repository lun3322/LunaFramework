using System;
using Luna.Dependency;
using Luna.Extensions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Luna.MongoDb
{
    public class LunaMongoDbClientManager : IScopedDependency, ILunaMongoDbClientManager
    {
        private readonly IConfiguration _configuration;
        private const string MONGODB_URL_SECTION_KEY = "MongoDb:Url";
        private const string MONGODB_DATABASE_SECTION_KEY = "MongoDb:Database";

        public LunaMongoDbClientManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IMongoCollection<T> GetMongoCollection<T>(string database = null, string collection = null)
        {
            var col = collection ?? nameof(T);
            var mongoDatabase = GetMongoDatabase(database);
            var mongoCollection = mongoDatabase.GetCollection<T>(col);
            return mongoCollection;
        }

        public IMongoClient GetMongoClient()
        {
            var url = _configuration.GetSection(MONGODB_URL_SECTION_KEY).Value;
            if (url.IsNullOrWhiteSpace())
            {
                throw new Exception($"Please configure the {MONGODB_URL_SECTION_KEY} section");
            }

            var mongoClient = new MongoClient(url);
            return mongoClient;
        }

        public IMongoDatabase GetMongoDatabase(string database = null)
        {
            var client = GetMongoClient();
            var db = database ?? _configuration.GetSection(MONGODB_DATABASE_SECTION_KEY).Value;
            if (db.IsNullOrWhiteSpace())
            {
                throw new Exception($"Please configure the {MONGODB_DATABASE_SECTION_KEY} section or set {nameof(database)} arguments");
            }

            return client.GetDatabase(db);
        }
    }
}