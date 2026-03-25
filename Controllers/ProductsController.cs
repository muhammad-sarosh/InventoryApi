using InventoryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService _productsService;

        public ProductsController(ProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            var products = _productsService.GetProducts();
            return Ok(products);
        }
    }
}