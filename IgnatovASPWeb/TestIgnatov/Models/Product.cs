using System.ComponentModel.DataAnnotations;

namespace TestIgnatov.Models
{
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid().ToString();
        }
        public Product(string id, string name, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}