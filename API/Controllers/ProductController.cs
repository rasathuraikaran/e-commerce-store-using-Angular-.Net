using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers


{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        public readonly StoreContext Context;

        public ProductsController(StoreContext context)
        {
            this.Context = context;
  
        }

        [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await Context.Products.ToListAsync();
    }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await Context.Products.FindAsync(id);

            if (product == null) return NotFound();

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            Context.Products.Add(product);
            await Context.SaveChangesAsync();
            return product;
        }

 
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.Id || !ProductExists(id)) return BadRequest("Cannot update this product");

      Context.Entry(product).State= EntityState.Modified;

        await Context.SaveChangesAsync();

        return NoContent ();
    }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await Context.Products.FindAsync(id);


            if (product == null) return NotFound();

Context.Products.Remove(product);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    
     private bool ProductExists(int id)
    {
            return Context.Products.Any(x => x.Id == id);    }

    }
}