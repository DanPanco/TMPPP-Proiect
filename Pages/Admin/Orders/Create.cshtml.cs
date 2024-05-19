using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.Models;
using Store.Pages.Admin.Orders.Facade;
using static Store.Pages.Admin.Orders.Facade.OrderFacade;

namespace Store.Pages.Admin.Orders
{
    public class CreateModel : PageModel
    {
        private readonly OrderFacade _orderFacade;
        private readonly UserManager<User> _userManager;
        public List<Product> Products { get; set; }

        public CreateModel(OrderFacade orderFacade, UserManager<User> userManager)
        {
            _orderFacade = orderFacade;
            _userManager = userManager;
            Products = new List<Product>();
        }

        [BindProperty]
        public int ProductId { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task OnGetAsync()
        {
            if (Products == null)
            {
                Products = new List<Product>();
            }
            Products = await _orderFacade.GetAllProductsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            try
            {
                var order = await _orderFacade.PlaceOrder(user.UserName, ProductId, Quantity);
                return RedirectToPage("./Index");
            }
            catch (NotEnoughInStockException ex)
            {
                TempData["Message"] = ex.Message;
                return Page();
            }
        }
    }
}
