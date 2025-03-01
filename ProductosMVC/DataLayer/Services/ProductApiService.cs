using ProductosMVC.DataLayer.Interfaces;
using ProductosMVC.Models;

namespace ProductosMVC.DataLayer.Services;

using Newtonsoft.Json;

public class ProductApiService : IProductRepository
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ProductApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _httpClient.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var response = await _httpClient.GetAsync("products");
        response.EnsureSuccessStatusCode();

        // Read the content as a string
        var jsonString = await response.Content.ReadAsStringAsync();

        // Deserialize using Newtonsoft.Json
        var productos = JsonConvert.DeserializeObject<IEnumerable<Product>>(jsonString);

        // Check if productos is null and return an empty list if so
        return productos ?? Enumerable.Empty<Product>();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"products/{id}");
        response.EnsureSuccessStatusCode();

        // Read the content as a string
        var jsonString = await response.Content.ReadAsStringAsync();

        // Deserialize using Newtonsoft.Json
        var producto = JsonConvert.DeserializeObject<Product>(jsonString);

        // Return the producto, which might be null if not found
        return producto;
    }
}
