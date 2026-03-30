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

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProductById(int id)
        {
            var product = _productsService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult<ProductDto> AddProduct([FromBody] ProductDto product)
        {
            var createdProduct = _productsService.AddProduct(product);

            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }
    }
}