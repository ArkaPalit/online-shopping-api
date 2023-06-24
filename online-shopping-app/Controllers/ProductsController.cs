using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_shopping_app.Models;
using online_shopping_app.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace online_shopping_app.Controllers
{
    [Route("/api/v1.0/shopping")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;   
        }
        [Authorize]
        [HttpGet("all")]
        public ActionResult<List<Product>> GetAllProducts()
        {
            return Ok(new { data = productService.GetAllProducts(), success = true, messaege = "Request is successful" });
        }
        [Authorize]
        [HttpGet("products/{id}")]
        public ActionResult<Product> GetProductById(string id)
        {
            var product = productService.GetProductById(id);
            if (product == null)
            {
                return NotFound(new {success = false, message = "Invalid request" });
            }
            return Ok(new { data = product, success = true, messaege = "Request is successful" });
        }
        [Authorize]
        [HttpGet("products/search/{keyword}")]
        public ActionResult<List<Product>> GetProductsByKeyword(string keyword)
        {
            if(keyword == null)
            {
                return NotFound(new { success = false, message = "Invalid request" });
            }
            return Ok(new { data = productService.GetProductsByKeyword(keyword), success = true, messaege = "Request is successful" });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{productName}/add")]
        public ActionResult<Product> AddNewProduct([FromBody] Product product, string productName)
        {
            productService.AddNewProduct(product);
            return Ok(new { data = productService.GetProductById(product.ProductId), success = true, messaege = "Request is successful" });
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{productName}/update/{id}")]
        public ActionResult UpdateProduct(string productName, string id, [FromBody] Product product)
        {
            var existingProduct = productService.GetProductById(id);
            if(existingProduct == null)
            {
                return NotFound(new { success = false, message = "Invalid request" });
            }
            else
            {
                productService.UpdateProduct(productName, id, product);
                return Ok(new { success = true, messaege = "Request is successful" });
            }
           
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{productName}/delete/{id}")]
        public ActionResult RemoveProduct(string productName, string id)
        {
            var product = productService.GetProductById(id);
            if(product == null )
            {
                return NotFound(new { success = false, message = "Invalid request" });
            }
            productService.RemoveProduct(productName, id);
            return Ok(new { success = true, messaege = "Request is successful" });
        }
    }
}
