using Microsoft.AspNetCore.Mvc;
using ProductosMVC.DataLayer.Interfaces;
using ProductosMVC.Models;
using ProductosMVC.BusinessLayer;
using System.Threading.Tasks;

namespace ProductosMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductBusinessLogic _productBusinessLogic;

        public ProductsApiController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _productBusinessLogic = new ProductBusinessLogic();
        }

        // GET: api/ProductsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetAllProductsAsync();

                if (products == null || !products.Any())
                {
                    return NotFound("No products found.");
                }

                // Calculate tax for each product
                _productBusinessLogic.CalculateAndAssignTaxesMultipleProducts(ref products);
                return Ok(products);
            }
            catch (Exception)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, "An error occurred while retrieving products.");
            }
        }


        // GET: api/ProductsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);

                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                // Calculate tax for the product
                _productBusinessLogic.CalculateAndAssignTaxes(ref product);
                return Ok(product);
            }
            catch (Exception)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, $"An error occurred while retrieving the product with ID {id}.");
            }
        }
    }
}
