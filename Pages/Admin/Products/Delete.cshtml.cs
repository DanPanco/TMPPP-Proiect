using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Store.Services;
using Store.Models;

namespace Store.Pages.Admin.Products
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Product Product { get; set; }

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FindAsync(id);

            if (Product != null)
            {

                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();

                if (Product.ImageFileName != "DigitalClock.jpg" && Product.ImageFileName != "PocketCalculator.jpg" && Product.ImageFileName != "PortableRadio.jpg")
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/products", Product.ImageFileName);
                    try
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to delete file: {ex.Message}");
                    }
                }
            }

            return RedirectToPage("./Index");
        }
    }

}
