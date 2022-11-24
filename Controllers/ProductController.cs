using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;        
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> getProductDetails()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<List<Product>>> addProductDetails(Product NewProduct)
        {
            _context.Products.Add(NewProduct);
            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
        }

        [HttpPut]
        public async Task<ActionResult<List<Product>>> updateProductDetails(Product NewProduct)
        {
            var updateProduct = _context.Products.Find(NewProduct.productId);
            if (updateProduct == null)
            {
                return BadRequest("Product not found!!!");
            }
            updateProduct.productName = NewProduct.productName;
            updateProduct.productPrice = NewProduct.productPrice;
            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
        }

        [HttpDelete]
        public async Task<ActionResult<List<Product>>> removeProductDetails(int ProductId)
        {
            var updateProduct = _context.Products.Find(ProductId);
            if (updateProduct == null)
            {
                return BadRequest("Product not found!!!");
            }
            _context.Products.Remove(updateProduct);
            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
        }
    }
}
