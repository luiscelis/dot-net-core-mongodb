using System;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;

using System.Collections.Generic;

namespace TodoAPi.Models
{
    public class DataAccess
    {
        MongoClient _client;
        MongoServerAddress _server;
        IMongoDatabase _db;

        public DataAccess()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _db = _client.GetDatabase("Product");
        }

        public IEnumerable<Product> GetProducts()
        {

            IMongoCollection<Product> _products = _db.GetCollection<Product>("Products");
            return _products.FindSync<Product>().ToList<Product>; // _db.GetCollection<Product>("Products").FindAll();
        }


        public Product GetProduct(ObjectId id)
        {
            var res = Query<Product>.EQ(p => p.Id, id);
            return _db.GetCollection<Product>("Products").FindOne(res);
        }

        public Product Create(Product p)
        {
            _db.GetCollection<Product>("Products").Save(p);
            return p;
        }

        public void Update(ObjectId id, Product p)
        {
            p.Id = id;
            var res = Query<Product>.EQ(pd => pd.Id, id);
            var operation = Update<Product>.Replace(p);
            _db.GetCollection<Product>("Products").Update(res, operation);
        }
        public void Remove(ObjectId id)
        {
            var res = Query<Product>.EQ(e => e.Id, id);
            var operation = _db.GetCollection<Product>("Products").Remove(res);
        }
    }
}