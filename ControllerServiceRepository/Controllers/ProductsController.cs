using ControllerServiceRepository.Entities;
using ControllerServiceRepository.Models.DTOs.Requests;
using ControllerServiceRepository.Models.DTOs.Responses;
using ControllerServiceRepository.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControllerServiceRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ProductService _service = new ProductService();

        private List<Product> _products =
        [
            new Product
            {
                Id = 1,
                Name = "coca-cola",
                Price = 89,
            },
            new Product
            {
            Id = 2,
            Name = " galletita",
            Price = 999,
            }

        ];
        //obtner todos los productos
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_products);
        }

        // obtener productos con id
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            ProductForReadDto product = _service.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }

        // crear 
        [HttpPost]
        public IActionResult CreateProduct(ProductForCreateDto data)
        {
            if (_service.NameExists(data.Name))
            {
                return Conflict("Ya existe un producto con ese nombre.");
            }

            ProductForReadDto created = _service.CreateProduct(data);

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = created.Id },
                created
            );
        }
        // update
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductForUpdateDto data)
        { 
            if (_service.GetProductById(id) == null)
            {
                return NotFound();
            }

            _service.UpdateProduct(id,data);
            return NoContent();
        }
        // borrar

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            Product product = _products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _service.DeleteProduct(id);

            return NoContent();
        }
        
        // buscar producto 
        [HttpGet("search")]
        public IActionResult SearchProducts(string name)
        {
            return Ok(_service.SearchProductByName(name));
        }


        [HttpGet("stats")]
        public IActionResult GetProductStats()
        {
            ProductStatsDto stats = _service.GetStats();
            return Ok(stats);
        }


    }
}
