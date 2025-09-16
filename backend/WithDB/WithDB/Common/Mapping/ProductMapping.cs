using WithDB.DTOs.Products;
using WithDB.Models;

namespace WithDB.Common.Mapping
{
    public static class ProductMapping
    {
        public static ProductDto ToDto(this Product p) => new()
        {
            Id = p.Id,
            Name = p.Name,
            Photo = p.Photo,
            Quantity = p.Quantity,
            Price = p.Price,
            Description = p.Description
        };

        public static Product FromCreateDto(this CreateProductDto dto) => new()
        {
            Name = dto.Name,
            Photo = dto.Photo,
            Quantity = dto.Quantity,
            Status = dto.Quantity > 0 ? true : false,
            Price = dto.Price,
            Description = dto.Description
        };

        public static void MapUpdate(this Product entity, UpdateProductDto dto)
        {
            entity.Name = dto.Name;
            entity.Photo = dto.Photo;
            entity.Quantity = dto.Quantity;
            entity.Status = dto.Quantity > 0 ? true : false;
            entity.Price = dto.Price;
            entity.Description = dto.Description;
        }
    }
}
