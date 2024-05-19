using Store.Models;

namespace Store.Pages.Admin.Products
{
    public interface IProductFactory
    {
        Product CreateProduct(ProductType productType);
    }
}
