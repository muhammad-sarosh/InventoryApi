using System.Linq;

namespace InventoryApi.Services
{
    public class ProductsService
    {
        private readonly List<ProductDto> _products = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Chips", Price = 500.0m, StockQuantity = 10 },
            new ProductDto { Id = 2, Name = "Soda", Price = 300.0m, StockQuantity = 15 },
            new ProductDto { Id = 3, Name = "Candy", Price = 50.0m, StockQuantity = 30 }
        };

        public List<ProductDto> GetProducts()
        {
            return _products;
        }

        public ProductDto? GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public ProductDto AddProduct(ProductDto product)
        {
            int nextId = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;

            var newProduct = new ProductDto
            {
                Id = nextId,
                Name = product.Name,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };

            _products.Add(newProduct);
            return newProduct;
        }
    }
}