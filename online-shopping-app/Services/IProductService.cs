using online_shopping_app.Models;

namespace online_shopping_app.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(string id);
        List<Product> GetProductsByKeyword(string keyword);
        Product AddNewProduct(Product product);
        void UpdateProduct(string productName, string id, Product product);
        void RemoveProduct(string productName, string id);
    }
}
