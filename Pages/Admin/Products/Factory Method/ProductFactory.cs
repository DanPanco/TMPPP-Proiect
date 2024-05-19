using Store.Models;

namespace Store.Pages.Admin.Products
{
    public class ProductFactory : IProductFactory
    {
        public Product CreateProduct(ProductType productType)
        {
            switch (productType)
            {
                case ProductType.Mouse:
                    return new MouseProductFactory().CreateProduct(productType);
                case ProductType.Headset:
                    return new HeadsetProductFactory().CreateProduct(productType);
                case ProductType.Keyboard:
                    return new KeyboardProductFactory().CreateProduct(productType);
                default:
                    throw new ArgumentException("Invalid course type", nameof(productType));
            }
        }
    }

    public class MouseProductFactory : IProductFactory
    {
        public Product CreateProduct(ProductType courseType)
        {
            if (courseType != ProductType.Mouse)
            {
                throw new ArgumentException("Only Mouse Type Items!");
            }
            return new Product
            {
                ProductType = ProductType.Mouse,
                Name = "HyperX Pulsefire Core",
                Brand = "HyperX",
                Price = 25,
                StockCount = 100,
                Description = "Solid gaming-grade optical mouse with customizable RGB lighting",
                ImageFileName = "Mouse.jpg",
                CreatedAt = DateTime.Now
            };
        }
    }

    public class HeadsetProductFactory : IProductFactory
    {
        public Product CreateProduct(ProductType courseType)
        {
            if (courseType != ProductType.Headset)
            {
                throw new ArgumentException("Only Headset Type Items!");
            }

            return new Product
            {
                ProductType = ProductType.Headset,
                Name = "HyperX Cloud Alpha - Gaming Headset",
                Brand = "HyperX",
                Price = 75,
                StockCount = 100,
                Description = "Get crystal-clear communication with your team and take your game to the next level with the Cloud Alpha audio advantage.",
                ImageFileName = "Headset.jpg",
                CreatedAt = DateTime.Now
            };
        }
    }

    public class KeyboardProductFactory : IProductFactory
    {
        public Product CreateProduct(ProductType courseType)
        {
            if (courseType != ProductType.Keyboard)
            {
                throw new ArgumentException("Only Keyboard Type Items!");
            }

            return new Product
            {
                ProductType = ProductType.Keyboard,
                Name = "HyperX Alloy FPS PRO",
                Brand = "HyperX",
                Price = 90,
                StockCount = 200,
                Description = "100% anti-ghosting, Ultra-portable design, Solid-steel frame, Convenient USB charge port, USB",
                ImageFileName = "Keyboard.jpg",
                CreatedAt = DateTime.Now
            };
        }
    }
}
