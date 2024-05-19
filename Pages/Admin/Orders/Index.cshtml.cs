using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.Models;
using Microsoft.EntityFrameworkCore;
using Store.Services;
using Microsoft.AspNetCore.Identity;

namespace Store.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Order> Order { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "admin");

            if (isAdmin)
            {
                Order = await _context.Orders.ToListAsync();
            }
            else
            {
                Order = await _context.Orders.Where(o => o.Username == user.UserName).ToListAsync();
            }
        }
    }
}
