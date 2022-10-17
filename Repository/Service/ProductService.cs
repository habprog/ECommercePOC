using Helper.MongoDBConnectionSetting;
using Helper.Wrappers;
using Microsoft.Extensions.Options;
using Models.Model;
using MongoDB.Driver;
using Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Service
{
    public class ProductService : IProductRepository
    {
        private readonly IMongoCollection<Product> _productsCollection;
        public ProductService(IOptions<MongoDBConnectionSetting> mongoDBConnectionSetting)
        {
            var mongoClient = new MongoClient(
            mongoDBConnectionSetting.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoDBConnectionSetting.Value.DatabaseName);

            _productsCollection = mongoDatabase.GetCollection<Product>(
                mongoDBConnectionSetting.Value.ProductsCollectionName);
        }
        public async Task<Response<dynamic>> GetProducts()
        {
            var response = new Response<dynamic>();
            var data =  await _productsCollection.Find(_ => true).ToListAsync();
            if(data.Count() > 0)
            {
                response.Succeeded = true;
                response.Message = "Record fectch successfully";
                response.Data = data;
            }
            else
            {
                response.Succeeded = true;
                response.Message = "No record found";
                response.Data = data;
            }
            return response;
        }

        public async Task<Response<dynamic>> Checkout(string id)
        {
            var response = new Response<dynamic>();
            var existProduct = await _productsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if(existProduct.Quantity < 1)
            {
                response.Succeeded = false;
                response.Message = "Product out of stock";
                return response;
            }
            var newQty = existProduct.Quantity - 1;
            existProduct.Quantity = newQty;
            await _productsCollection.ReplaceOneAsync(x => x.Id == id, existProduct);
            response.Succeeded = true;
            response.Message = "Product purchased successfully";
            return response;
        }
    }
}
