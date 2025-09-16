using System.ComponentModel.DataAnnotations;

namespace WithDB.DTOs.Products
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        [MaxLength(100, ErrorMessage = "Tên tối đa 100 ký tự")]
        public string Name { get; set; }
        public string? Photo {  get; set; }

        [Range(1, 1000, ErrorMessage = "Số lượng > 0")]
        public int Quantity { get; set; }

        [Range(1, 1_000_000, ErrorMessage = "Giá phải > 0")]
        public decimal Price { get; set; }

        [MaxLength(1000, ErrorMessage = "Mô tả tối đa 1000 ký tự")]
        public string? Description { get; set; }
    }
}
