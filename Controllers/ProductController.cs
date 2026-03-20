using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            var products = new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Chips", Price = 500.0m, StockQuantity = 10 },
                new ProductDto { Id = 2, Name = "Soda", Price = 300.0m, StockQuantity = 15 },
                new ProductDto { Id = 3, Name = "Candy", Price = 50.0m, StockQuantity = 30 }
            };

            return Ok(products);
        }
    }
}