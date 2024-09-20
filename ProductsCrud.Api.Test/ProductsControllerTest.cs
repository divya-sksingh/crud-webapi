using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductsCrud.DataModel;

namespace ProductsCrud.Api.Test
{
    [TestClass]
    public class ProductsControllerTest
    {
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<ILogger<ProductsController>> _mockLogger;
        private ProductsController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockLogger = new Mock<ILogger<ProductsController>>();
            _controller = new ProductsController(_mockLogger.Object, _mockProductRepository.Object);
        }

        // Test for AddNewProduct (POST)
        [TestMethod]
        public async Task AddNewProduct_Success_ReturnsOk()
        {
            // Arrange
            var product = new Product { Name = "Test Product", Price = 100 };
            _mockProductRepository.Setup(repo => repo.AddProduct(It.IsAny<Product>())).ReturnsAsync(true);

            // Act
            var result = await _controller.AddNewProduct(product);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            var okResult = result as OkResult;
            Assert.AreEqual(200, okResult?.StatusCode);
        }

        [TestMethod]
        public async Task AddNewProduct_Failure_ReturnsStatusCode500()
        {
            // Arrange
            var product = new Product { Name = "Test Product", Price = 100 };
            _mockProductRepository.Setup(repo => repo.AddProduct(It.IsAny<Product>())).ReturnsAsync(false);

            // Act
            var result = await _controller.AddNewProduct(product);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var statusCodeResult = result as ObjectResult;
            Assert.AreEqual(500, statusCodeResult?.StatusCode);
            Assert.AreEqual("An error occurred while adding the product.", statusCodeResult?.Value);
        }

        // Test for GetProducts (GET)
        [TestMethod]
        public async Task GetProducts_ReturnsListOfProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product {  Name = "Product 1", Price = 100 },
                new Product {  Name = "Product 2", Price = 200 }
            };
            _mockProductRepository.Setup(repo => repo.GetProducts()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetProducts();

            // Assert
            Assert.AreEqual(products.Count(), result.Count());
            CollectionAssert.AreEqual(products, result.ToList());
        }

        // Test for GetProductById (GET by ID)
        [TestMethod]
        public async Task GetProductById_ProductExists_ReturnsProduct()
        {
            // Arrange
            var product = new Product { Name = "Test Product", Price = 100 };
            _mockProductRepository.Setup(repo => repo.GetProductById(1)).ReturnsAsync(product);

            // Act
            ActionResult<Product> result = await _controller.GetProductById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(product, result?.Value);
        }

        [TestMethod]
        public async Task GetProductById_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            Product product = null;
            _mockProductRepository.Setup(repo => repo.GetProductById(1)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetProductById(1);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        // Test for UpdateProduct (PUT)
        [TestMethod]
        public async Task UpdateProduct_ProductExists_ReturnsOk()
        {
            // Arrange
            var product = new Product { Name = "Updated Product", Price = 150 };
            _mockProductRepository.Setup(repo => repo.UpdateProduct(1, product)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateProduct(1, product);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task UpdateProduct_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            var product = new Product { Name = "Updated Product", Price = 150 };
            _mockProductRepository.Setup(repo => repo.UpdateProduct(1, product)).ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateProduct(1, product);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        // Test for DecrementStock (PUT decrement)
        [DataTestMethod]
        [DataRow(1, 5, true, typeof(OkResult))]
        [DataRow(1, 5, false, typeof(BadRequestObjectResult))]
        public async Task DecrementStock_VariousScenarios(int productId, int quantity, bool repoResult, Type expectedResultType)
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.DecrementProductStock(productId, quantity)).ReturnsAsync(repoResult);

            // Act
            var result = await _controller.DecrementStock(productId, quantity);

            // Assert
            Assert.IsInstanceOfType(result, expectedResultType);
        }

        // Test for AddToStock (PUT increment)
        [DataTestMethod]
        [DataRow(1, 5, true, typeof(OkResult))]
        [DataRow(1, 5, false, typeof(NotFoundObjectResult))]
        public async Task AddToStock_VariousScenarios(int productId, int quantity, bool repoResult, Type expectedResultType)
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.IncrementProductStock(productId, quantity)).ReturnsAsync(repoResult);

            // Act
            var result = await _controller.AddToStock(productId, quantity);

            // Assert
            Assert.IsInstanceOfType(result, expectedResultType);
        }

        // Test for DeleteProduct (DELETE)
        [TestMethod]
        public async Task DeleteProduct_ProductExists_ReturnsOk()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.DeleteProductById(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteProduct(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteProduct_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.DeleteProductById(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteProduct(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual("Product not found.", notFoundResult?.Value);
        }
    }
}
