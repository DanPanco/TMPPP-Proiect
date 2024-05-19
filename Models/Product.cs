using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public enum ProductType
    {
        Mouse,
        Headset,
        Keyboard,
        CPU,
        GPU,
        RAM,
        Display,
        PowerSupply
    }

    public class Product
    {
        public int Id { get; set; }
        [BindProperty]
        public ProductType ProductType { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = "";
        [MaxLength(100)]
        public string Brand { get; set; } = "";
        [Precision(16, 2)]
        public virtual decimal Price { get; set; }
        public string Description { get; set; } = "";
        [MaxLength(100)]
        public string ImageFileName { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public int StockCount {  get; set; }

    }
}
