using Store.Models;

namespace Store.Pages.Admin.Products.Builder
{
    public class ProductBuilder : IProductBuilder
    {
        private Product _product = new Product();

        public IProductBuilder SetProductType(ProductType productType)
        {
            _product.ProductType = productType;
            return this;
        }
        public IProductBuilder SetName(string name)
        {
            _product.Name = name;
            return this;
        }

        public IProductBuilder SetBrand(string brand)
        {
            _product.Brand = brand;
            return this;
        }

        public IProductBuilder SetPrice(decimal price)
        {
            _product.Price = price;
            return this;
        }
        public IProductBuilder SetStockCount(int stockCount)
        {
            _product.StockCount = stockCount;
            return this;
        }

        public IProductBuilder SetDescription(string description)
        {
            _product.Description = description;
            return this;
        }

        public IProductBuilder SetImageFileName(string imageFileName)
        {
            _product.ImageFileName = imageFileName;
            return this;
        }

        public IProductBuilder SetProductDateTime(DateTime productDateTime)
        {
            _product.CreatedAt = productDateTime;
            return this;
        }

        public Product Build()
        {
            return _product;
        }
    }

}
