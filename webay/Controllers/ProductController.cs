using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webay.Models;

namespace webay.Controllers
{
    [Route("/api/[controller]")]
    public class ProductController : Controller
    {
        private static List<Product> _products = new List<Product>(new[] {
            new Product() { Id = 1, Name = "Television" },
            new Product() { Id = 2, Name = "Radio" },
            new Product() { Id = 3, Name = "Computer" },
        });

        [HttpGet]
        public IEnumerable<Product> Index()
        {
            return _products;
        }

        [HttpGet("{id}")]
        public IActionResult View(int id)
        {
            var product = _products.SingleOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            _products.Add(product);

            return CreatedAtAction(nameof(View), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Product product)
        {
            if (product == null || product.Id != id || !ModelState.IsValid)
                return BadRequest(ModelState);

            var _product = _products.FirstOrDefault(p => p.Id == id);

            if (_product == null)
                return NotFound(product);

            _product.Name = product.Name;

            return Ok(_product); 
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return BadRequest(product);
                    
            _products.Remove(product);

            return Ok(product);
        }
    }
}
