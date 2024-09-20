using Microsoft.AspNetCore.Mvc;
using ProductsCrud.DataModel;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ProductsCrud.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductRepository _productRepository;

        public ProductsController(ILogger<ProductsController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Api to create a new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Returns an Ok on success</returns>
        /// <remarks>
        /// ** Sample Request **
        /// 
        /// {
        ///     "name": "ProductA",
        ///     "price": 200,
        ///     "stockQuantity": 50
        /// }
        /// </remarks>
        [HttpPost()]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An error occurred while adding the product.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid input request body")]
        public async Task<IActionResult> AddNewProduct([Required] Product product)
        {
            bool result = await _productRepository.AddProduct(product);
            if (!result)
            {
                return StatusCode(500, "An error occurred while adding the product.");
            }
            return Ok();
        }

        /// <summary>
        /// Api to fetch all products
        /// </summary>
        /// <returns>Returns a List of products</returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            IEnumerable<Product> products = await _productRepository.GetProducts();
            return products;
        }

        /// <summary>
        /// Api to fetch the product details given its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the queried Product</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            Product product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        /// <summary>
        /// Api that updates the Product name/quantity/price given its id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns>Returns an Ok on success</returns>
        /// <remarks>
        ///  ** Sample Request **
        ///  
        /// {
        ///     "name": "ProductB",
        ///     "price": 100,
        ///     "stockQuantity": 5
        /// }
        /// </remarks>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            var result = await _productRepository.UpdateProduct(id, product);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        ///  Api to decrement the stock with the quantity provided in the input.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns>Returns an Ok on success</returns>
        [HttpPut("decrement-stock/{id}/{quantity}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DecrementStock(int id, int quantity)
        {
            bool result = await _productRepository.DecrementProductStock(id, quantity);
            if (!result)
            {
                return BadRequest("Insufficient stock or product not found.");
            }

            return Ok();
        }

        /// <summary>
        /// Api to add the stock with the quantity provided in the input. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns>Returns an Ok on success</returns>
        [HttpPut("add-to-stock/{id}/{quantity}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddToStock(int id, int quantity)
        {
            bool result = await _productRepository.IncrementProductStock(id, quantity);
            if (!result)
            {
                return NotFound("Product not found.");
            }

            return Ok();
        }

        /// <summary>
        /// Api to delete the product by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns an Ok on success</returns>
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productRepository.DeleteProductById(id);
            if (!result)
            {
                return NotFound("Product not found.");
            }

            return Ok();
        }

    }
}
