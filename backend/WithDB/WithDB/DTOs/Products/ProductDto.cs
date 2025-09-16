using Microsoft.EntityFrameworkCore;

namespace WithDB.DTOs.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo {  get; set; }
        public int Quantity { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
