using Microsoft.EntityFrameworkCore;
using ProductsCrud.DataModel;

namespace ProductsCrud.Api
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _productDbContext;
        public ProductRepository(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }
        public async Task<bool> AddProduct(Product product)
        {
            _productDbContext.Products.Add(product);
            return await _productDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DecrementProductStock(int id, int quantity)
        {
            Product product = await _productDbContext.Products.FindAsync(id);
            if (product == null || quantity > product.StockQuantity)
            {
                return false; // Product not found or insufficient stock
            }

            product.StockQuantity -= quantity;
            return await _productDbContext.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteProductById(int id)
        {
            var product = await _productDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return false; // Product not found
            }

            _productDbContext.Products.Remove(product);
            return await _productDbContext.SaveChangesAsync() > 0;
        }

        public async Task<Product> GetProductById(int id)
        {
            Product product = new();
            product = await _productDbContext.Products.FindAsync(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productDbContext.Products.ToListAsync();
        }

        public async Task<bool> IncrementProductStock(int id, int quantity)
        {
            var product = await _productDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return false; // Product not found
            }

            product.StockQuantity += quantity;
            return await _productDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProduct(int id, Product product)
        {
            var existingProduct = await _productDbContext.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return false; // Product not found
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;

            return await _productDbContext.SaveChangesAsync() > 0;
        }
    }
}
