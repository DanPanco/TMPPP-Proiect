using static Store.Pages.Admin.Orders.Facade.OrderFacade;
using Store.Services;
using Store.Models;

namespace Store.Pages.Admin.Orders.Chain_Of_Responsability
{
    public interface IOrderHandler
    {
        IOrderHandler Next { get; set; }
        Task<Order?> HandleOrder(string username, int productId, int quantity);
    }

    public class StockVerificationHandler : IOrderHandler
    {
        private readonly ApplicationDbContext _context;
        public IOrderHandler Next { get; set; }

        public StockVerificationHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> HandleOrder(string username, int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null || product.StockCount < quantity)
            {
                throw new NotEnoughInStockException(product.Name, product.StockCount);
            }

            return await (Next?.HandleOrder(username, productId, quantity));
        }
    }

    public class OrderPlacementHandler : IOrderHandler
    {
        private readonly ApplicationDbContext _context;
        public IOrderHandler Next { get; set; }

        public OrderPlacementHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> HandleOrder(string username, int productId, int quantity)
        {
            var order = new Order { Username = username, ProductId = productId, Quantity = quantity, OrderDate = DateTime.Now };
            _context.Orders.Add(order);

            return await (Next?.HandleOrder(username, productId, quantity));
        }
    }

    public class StockUpdateHandler : IOrderHandler
    {
        private readonly ApplicationDbContext _context;
        public IOrderHandler Next { get; set; }

        public StockUpdateHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> HandleOrder(string username, int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            product.StockCount -= quantity;
            _context.Products.Update(product);

            await _context.SaveChangesAsync();

            if (Next != null)
            {
                return await Next.HandleOrder(username, productId, quantity);
            }
            else
            {
                return null;
            }
        }
    }
}
