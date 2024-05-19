using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.Models;
using Store.Services;
using Store.Pages.Admin.Products;
using Store.Pages.Admin.Products.Prototype;
using Microsoft.AspNetCore.Identity;
using Store.Pages.Admin.Products.Strategy;
using Microsoft.EntityFrameworkCore;

namespace Store.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly IProductFactory productFactory;
        private readonly ProductPrototype productPrototype;
        private readonly UserManager<User> _userManager;
        public List<Product> Products { get; set; } = new List<Product>();
        [BindProperty]
        public ProductType ProductType { get; set; }

        public IndexModel(ApplicationDbContext context, IProductFactory productFactory, ProductPrototype productPrototype, UserManager<User> userManager)
        {
            this.context = context;
            this.productFactory = productFactory;
            this.productPrototype = productPrototype;
            this._userManager = userManager;
        }

        public ISortStrategy SortStrategy { get; set; }
        public void SetSortStrategy(ISortStrategy sortStrategy)
        {
            SortStrategy = sortStrategy;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                Product newProduct;
                if (ProductType == ProductType.Mouse || ProductType == ProductType.Headset || ProductType == ProductType.Keyboard)
                {
                    newProduct = productFactory.CreateProduct(ProductType);
                }
                else
                {
                    return RedirectToPage("/Admin/Products/Create");
                }

                context.Products.Add(newProduct);
                context.SaveChanges();
                return RedirectToPage("/Admin/Products/Index");
            }

            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return Page();
            }
        }

        public IActionResult OnPostClone(int id)
        {
            var originalProduct = context.Products.Find(id);
            if (originalProduct == null)
            {
                return NotFound();
            }

            var clonedProduct = productPrototype.CloneProduct(originalProduct);
            context.Products.Add(clonedProduct);
            context.SaveChanges();

            return RedirectToPage("/Admin/Products/Index");
        }

        public async Task OnGetAsync(int? Id, string sortStrategy)
        {
            switch (sortStrategy)
            {
                case "stockcountAscending":
                    SetSortStrategy(new SortByStockCountAscending());
                    break;
                case "stockcountDescending":
                    SetSortStrategy(new SortByStockCountDescending());
                    break;
                case "priceAscending":
                    SetSortStrategy(new SortByPriceAscending());
                    break;
                case "priceDescending":
                    SetSortStrategy(new SortByPriceDescending());
                    break;
            }

            if (Id.HasValue)
            {
                Products = await context.Products
                    .Where(r => r.Id == Id.Value)
                    .ToListAsync();
            }
            else
            {
                Products = await context.Products
                    .ToListAsync();
            }

            if (SortStrategy != null)
            {
                Products = context.Products.OrderByDescending(p => p.Id).ToList();
            }
        }
    }
}
