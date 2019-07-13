using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly StoreDataContext _context;

        public CategoryController(StoreDataContext context)
        {
            _context = context;
        }

        [Route("v1/categories")]
        [HttpGet]
        public IEnumerable<Category> Get() => _context.Categorys.AsNoTracking().ToList();

        [Route("v1/categories/{id}")]
        [HttpGet]
        public Category Get(int id) => _context.Categorys.AsNoTracking().Where(c => c.Id == id).FirstOrDefault();

        [Route("v1/categories/{id}/products")]
        [HttpGet]
        public IEnumerable<Product> GetProducts(int id) => _context.Products.AsNoTracking().Where(p => p.CategoryId == id).ToList();

        [Route("v1/categories")]
        [HttpPost]
        public Category Post([FromBody] Category category)
        {
            _context.Categorys.Add(category);
            _context.SaveChanges();

            return category;
        }

        [Route("v1/categories")]
        [HttpPut]
        public Category Put([FromBody] Category category)
        {
            _context.Entry<Category>(category).State = EntityState.Modified;
            _context.SaveChanges();

            return category;
        }

        [Route("v1/categories")]
        [HttpDelete]
        public Category Delete([FromBody] Category category)
        {
            _context.Categorys.Remove(category);
            _context.SaveChanges();

            return category;
        }
    }
}