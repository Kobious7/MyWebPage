using System.ComponentModel.DataAnnotations;

namespace WithDB.DTOs.Products
{
    public class UpdateProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string? Photo {  get; set; }

        public int Quantity { get; set; }

        [Range(0, 1000000)]
        public decimal Price { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }
    }
}
