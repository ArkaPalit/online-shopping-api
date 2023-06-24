using online_shopping_app.Models;
using MongoDB.Driver;

namespace online_shopping_app.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _products = database.GetCollection<Product>(settings.CollectionsName);
        }
        public Product AddNewProduct(Product product)
        {
            if(product.QuantityAvaiable > 0)
            {
                product.ProductStatus = "HURRY UP TO PURCHASE";
                _products.InsertOne(product);
                return product;
            }
            else
            {
                product.ProductStatus = "‘OUT OF STOCK";
                _products.InsertOne(product);
                return product;
            }
            
        }
        public List<Product> GetAllProducts()
        {
            return _products.Find(product => true ).ToList();
        }
        public Product GetProductById(string id)
        {
            return _products.Find(product => product.ProductId == id).FirstOrDefault();
        }
        public List<Product> GetProductsByKeyword(string keyword)
        {
            return _products.Find(products => products.ProductName.ToLower().Contains(keyword.ToLower())).ToList();
        }
        public void RemoveProduct(string productName, string id)
        {
            _products.DeleteOne(product => product.ProductName == productName && product.ProductId == id);
        }
        public void UpdateProduct(string productName, string id, Product product)
        {
            if(product.QuantityAvaiable > 0)
            {
                product.ProductStatus = "HURRY UP TO PURCHASE";
            }
            else
            {
                product.ProductStatus = "OUT OF STOCK";
            }
            _products.ReplaceOne(product => product.ProductName == productName && product.ProductId == id, product);
        }
    }
}
