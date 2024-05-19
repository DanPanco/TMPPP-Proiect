using Store.Models;

namespace Store.Pages.Admin.Products.Prototype
{
    public class ProductPrototype
    {
        public Product CloneProduct(Product originalProduct)
        {
            return new Product
            {
                Id = 0,
                ProductType = originalProduct.ProductType,
                Name = originalProduct.Name,
                Brand = originalProduct.Brand,
                Price = originalProduct.Price,
                StockCount = originalProduct.StockCount,
                Description = originalProduct.Description,
                ImageFileName = originalProduct.ImageFileName,
                CreatedAt = originalProduct.CreatedAt,
            };
        }
    }

}
