using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WithDB.Models;
using WithDB.Common.Mapping;
using WithDB.DTOs.Products;
using WithDB.Common.Responses;

namespace WithDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>GET api/products?search=&page=1&pageSize=10</summary>
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Products.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }

            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var data = items.Select(p => p.ToDto()).ToList();

            return Ok(ApiResponse<object>.Ok(new
            {
                total,
                page,
                pageSize,
                items = data
            }, "Get products successfully"));
        }

        /// <summary>GET api/products/{id}</summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetById([FromRoute] int id)
        {
            var entity = await _context.Products.FindAsync(id);

            if (entity == null)
            {
                return NotFound(ApiResponse<string>.Fail("Product not found"));
            }

            return Ok(ApiResponse<ProductDto>.Ok(entity.ToDto(), "Get product successfully"));
        }

        /// <summary>POST api/products</summary>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var entity = dto.FromCreateDto();
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            var result = entity.ToDto();

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, ApiResponse<ProductDto>.Ok(result, "Product created"));
        }

        /// <summary>PUT api/products/{id}</summary>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductDto dto)
        {
            var entity = await _context.Products.FindAsync(id);

            if (entity == null)
                return NotFound(ApiResponse<string>.Fail("Product not found"));

            entity.MapUpdate(dto);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<ProductDto>.Ok(entity.ToDto(), "Product updated"));
        }

        /// <summary>PATCH api/products/{id}</summary>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] Dictionary<string, object> patch)
        {
            var entity = await _context.Products.FindAsync(id);

            if (entity == null)
                return NotFound(ApiResponse<string>.Fail("Product not found"));

            if (patch.TryGetValue("name", out var nameVal) && nameVal is string name)
                entity.Name = name;

            if (patch.TryGetValue("price", out var priceVal) && decimal.TryParse(priceVal?.ToString(), out var price))
                entity.Price = price;

            if (patch.TryGetValue("description", out var desVal))
                entity.Description = desVal?.ToString();

            await _context.SaveChangesAsync();

            return Ok(ApiResponse<ProductDto>.Ok(entity.ToDto(), "Product patched"));
        }

        /// <summary>DELETE api/products/{id}</summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var entity = await _context.Products.FindAsync(id);

            if (entity == null)
            {
                return NotFound(ApiResponse<string>.Fail("Product not found"));
            }

            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<string>.Ok("Deleted", "Product deleted"));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
