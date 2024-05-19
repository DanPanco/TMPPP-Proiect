using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.Services;
using Store.Models;
using Store.Pages.Admin.Products.Builder;

namespace Store.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductBuilder _productBuilder;

        [BindProperty]
        public ProductType ProductType { get; set; }
        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public CreateModel(ApplicationDbContext context, IProductBuilder productBuilder)
        {
            _context = context;
            _productBuilder = productBuilder;
        }

        public IActionResult OnGet()
        {
            return Page();
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

                var fileName = ProcessUploadedFile();

                if (fileName != null)
                {
                    newProduct = _productBuilder
                        .SetProductType(ProductType)
                        .SetName(Request.Form["Name"])
                        .SetBrand(Request.Form["Brand"])
                        .SetPrice(decimal.Parse(Request.Form["Price"]))
                        .SetStockCount(int.Parse(Request.Form["StockCount"]))
                        .SetDescription(Request.Form["Description"])
                        .SetImageFileName(fileName)
                        .SetProductDateTime(DateTime.Now)
                        .Build();
                }
                else
                {
                    throw new Exception("Image file is required.");
                }

                _context.Products.Add(newProduct);
                _context.SaveChanges();

                return RedirectToPage("/Admin/Products/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return Page();
            }
        }
        private string ProcessUploadedFile()
        {
            if (ImageFile == null)
            {
                return null;
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/products", uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }

            return uniqueFileName;
        }

    }
}
