using System.Linq;

namespace InventoryApi.Services
{
    public class ProductsService
    {
        private readonly InventoryDbContext _dbContext;

        public ProductsService(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly List<ProductDto> _products = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Chips", Price = 500.0m, StockQuantity = 10 },
            new ProductDto { Id = 2, Name = "Soda", Price = 300.0m, StockQuantity = 15 },
            new ProductDto { Id = 3, Name = "Candy", Price = 50.0m, StockQuantity = 30 }
        };

        public List<ProductDto> GetProducts()
        {
            return _dbContext.Products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity
                })
                .ToList();
        }

        public ProductDto? GetProductById(int id)
        {
            return _dbContext.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity
                })
                .FirstOrDefault();
        }

        public ProductDto AddProduct(ProductDto product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };

            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();

            return new ProductDto
            {
                Id = newProduct.Id,
                Name = newProduct.Name,
                Price = newProduct.Price,
                StockQuantity = newProduct.StockQuantity
            };
        }

        public bool DeleteProduct(int id)
        {
            ProductDto? product = _products.FirstOrDefault(p => p.Id == id);
            
            if (product == null)
            {
                return false;
            }

            _products.Remove(product);
            return true;
        }
        
        public bool UpdateProduct(int id, ProductDto updatedProduct)
        {
            var existingProduct = _dbContext.Products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.StockQuantity = updatedProduct.StockQuantity;

            _dbContext.SaveChanges();

            return true;
        }
    }
}