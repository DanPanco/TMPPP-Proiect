using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.Pages.Admin.Orders.Chain_Of_Responsability;
using Store.Services;

namespace Store.Pages.Admin.Orders.Facade
{
    public class OrderFacade
    {
        private readonly ApplicationDbContext _context;

        public OrderFacade(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public class NotEnoughInStockException : Exception
        {
            public NotEnoughInStockException(string productName, int remaining) : base($"Not enough in stock for {productName}. Only {remaining} left.") { }
        }

        public async Task<Order?> PlaceOrder(string username, int productId, int quantity)
        {
            IOrderHandler handler = new StockVerificationHandler(_context);
            handler.Next = new OrderPlacementHandler(_context);
            handler.Next.Next = new StockUpdateHandler(_context);

            return await handler.HandleOrder(username, productId, quantity);
        }
    }
}
