using Store.Models;

namespace Store.Pages.Admin.Products.Strategy
{
    public interface ISortStrategy
    {
        IList<Product> Sort(IList<Product> reviews);
    }

    public class SortByStockCountAscending : ISortStrategy
    {
        public IList<Product> Sort(IList<Product> reviews)
        {
            return reviews.OrderBy(r => r.StockCount).ToList();
        }
    }

    public class SortByStockCountDescending : ISortStrategy
    {
        public IList<Product> Sort(IList<Product> reviews)
        {
            return reviews.OrderByDescending(r => r.StockCount).ToList();
        }
    }

    public class SortByPriceAscending : ISortStrategy
    {
        public IList<Product> Sort(IList<Product> reviews)
        {
            return reviews.OrderBy(r => r.Price).ToList();
        }
    }

    public class SortByPriceDescending : ISortStrategy
    {
        public IList<Product> Sort(IList<Product> reviews)
        {
            return reviews.OrderByDescending(r => r.Price).ToList();
        }
    }
}
