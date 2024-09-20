using ProductsCrud.DataModel;

namespace ProductsCrud.Api
{
    /// <summary>
    /// Defines a repository for managing product-related data operations.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Adds a new product to the repository
        /// </summary>
        /// <param name="product">The product entity to be added.</param>
        /// <returns>A Task representing the asynchronous operation, containing a boolean indicating success or failure.</returns>
        public Task<bool> AddProduct(Product product);

        /// <summary>
        /// Fetches all products from the repository
        /// </summary>
        /// <returns>A Task representing the asynchronous operation, containing an enumerable collection of products</returns>
        public Task<IEnumerable<Product>> GetProducts();

        /// <summary>
        /// Retrieves a product by its unique id
        /// </summary>
        /// <param name="id">The unique id of the product.</param>
        /// <returns>A Task representing the asynchronous operation, containing the product entity if found, or null otherwise</returns>
        public Task<Product> GetProductById(int id);

        /// <summary>
        /// Updates the details of an existing product.
        /// </summary>
        /// <param name="id">The unique id of the product to be updated.</param>
        /// <param name="product">The updated product entity</param>
        /// <returns>A Task representing the asynchronous operation, containing a boolean indicating success or failure</returns>
        public Task<bool> UpdateProduct(int id, Product product);

        /// <summary>
        /// Increments the stock quantity of a product.
        /// </summary>
        /// <param name="id">The unique id of the product.</param>
        /// <param name="quantity">The quantity to add to the current stock.</param>
        /// <returns>A Task representing the asynchronous operation, containing a boolean indicating success or failure</returns>
        public Task<bool> IncrementProductStock(int id, int quantity);

        /// <summary>
        /// Decrements the stock quantity of a product.
        /// </summary>
        /// <param name="id">The unique id of the product.</param>
        /// <param name="quantity">The quantity to subtract from the current stock.</param>
        /// <returns>A Task representing the asynchronous operation, containing a boolean indicating success or failure. Returns false if insufficient stock.</returns>
        public Task<bool> DecrementProductStock(int id, int quantity);

        /// <summary>
        /// Deletes a product from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The unique id of the product to be deleted.</param>
        /// <returns>A Task representing the asynchronous operation, containing a boolean indicating success or failure</returns>
        public Task<bool> DeleteProductById(int id);
    }

}
