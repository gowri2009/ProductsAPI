using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsAPI.Data;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    public class ProductsController : Controller
    {

        private readonly DataContext _dataContext;

        public ProductsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [Route("api/products")]
        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var movies = _dataContext.Products.ToList();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }


        }

        [Route("api/getproduct/{id}")]
        [HttpGet]
        public IActionResult GetProduct(int id)
        {
            try
            {

                var product = _dataContext.Products.FirstOrDefault(x => x.Id == id);
                if (product == null) return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [Route("api/createproduct")]
        [HttpPost]

        public IActionResult AddProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {

                var productexists = _dataContext.Products.Any(n => n.Name.Equals(product.Name));
                if (productexists) return BadRequest("Product already exists");

                _dataContext.Products.Add(product);
                _dataContext.SaveChanges();

                return Ok("Product was added");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Route("updateproduct/{id}")]
        [HttpPut]

        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != product.Id) return BadRequest();

            try
            {

                var editprod = _dataContext.Products.FirstOrDefault(x => x.Id == id);
                if (editprod == null) return NotFound();


                editprod.Name = product.Name;
                editprod.Description = product.Description;
                editprod.Price = product.Price;

                //context.Products.Update(editprod);

                _dataContext.SaveChanges();
                return Ok("Product Updated");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Route("deleteproduct/{id}")]
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            try
            {

                var product = _dataContext.Products.FirstOrDefault(x => x.Id == id);
                if (product == null) return NotFound();

                _dataContext.Products.Remove(product);
                _dataContext.SaveChanges();

                return Ok("Product Deleted");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
