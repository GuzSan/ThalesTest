using ProductosMVC.Models;

namespace ProductosMVC.BusinessLayer;

public class ProductBusinessLogic
{
    private const decimal TaxRate = 0.19m;

    // Method to calculate tax for a product
    public decimal CalculateTax(decimal price)
    {
        return price * TaxRate;
    }

    public void CalculateAndAssignTaxes(ref Product product)
    {
        product.Tax = CalculateTax(product.Price);
        product.PriceWithTaxes = product.Price + product.Tax;
    }

    public void CalculateAndAssignTaxesMultipleProducts(ref IEnumerable<Product> products)
    {
        foreach (var product in products)
        {
            product.Tax = CalculateTax(product.Price);
            product.PriceWithTaxes = product.Price + product.Tax;
        }
    }

}
