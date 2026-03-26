namespace InventoryApi.Services
{
    public class ProductsService
    {
        public List<ProductDto> GetProducts()
        {
            return new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Chips", Price = 500.0m, StockQuantity = 10},
                new ProductDto { Id = 2, Name = "Soda", Price = 300.0m, StockQuantity = 15 },
                new ProductDto { Id = 3, Name = "Candy", Price = 50.0m, StockQuantity = 30 }
            };
        }

        public ProductDto? GetProductById(int id)
        {
            return GetProducts().FirstOrDefault(p => p.Id == id);
        }
    }
}
