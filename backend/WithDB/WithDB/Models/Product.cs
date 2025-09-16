using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WithDB.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Photo {  get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
