using Microsoft.AspNetCore.Mvc;
using ProductosMVC.DataLayer.Interfaces;
using ProductosMVC.Models;
using ProductosMVC.BusinessLayer;
using System.Diagnostics;

namespace ProductosMVC.Controllers;

public class ProductsController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ProductBusinessLogic _productBusinessLogic;


    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        _productBusinessLogic = new ProductBusinessLogic(); 
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productRepository.GetAllProductsAsync();

        if (products is null || !products.Any())
            return NotFound();

        _productBusinessLogic.CalculateAndAssignTaxesMultipleProducts(ref products);
        return View(products);
    }

    public async Task<IActionResult> AllProducts()
    {
        var products = await _productRepository.GetAllProductsAsync();  

        if (products is null || !products.Any())
            return NotFound();

        _productBusinessLogic.CalculateAndAssignTaxesMultipleProducts(ref products);
        return View(products);
    }
    
    public async Task<IActionResult> Details(int id)
    {
        Product? product = await _productRepository.GetProductByIdAsync(id);

        if (product is null)
            return NotFound();

        _productBusinessLogic.CalculateAndAssignTaxes(ref product);
        return View(product);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}