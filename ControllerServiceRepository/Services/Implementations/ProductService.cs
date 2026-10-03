using ControllerServiceRepository.Entities;
using ControllerServiceRepository.Models.DTOs.Requests;
using ControllerServiceRepository.Models.DTOs.Responses;
using ControllerServiceRepository.Repositories.Implementations;
using ControllerServiceRepository.Repositories.Interfaces;
using ControllerServiceRepository.Services.Interfaces;

namespace ControllerServiceRepository.Services.Implementations;

public class ProductService : IProductService
{  private readonly ProductRepository _repository;
    
    public ProductService()
    {
        _repository = new ProductRepository();
    }

    public List<ProductForReadDto> GetAllProducts()
    {
        var products = _repository.GetAllProducts();
        return products.Select(product => new ProductForReadDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        }).ToList();
    }

    public ProductForReadDto? GetProductById(int id)
    {
        var products = _repository.GetProductById(id);
        if( products == null)
        {
            return null;
        }

        return new ProductForReadDto()
        {
            Id = products.Id,
            Name = products.Name,
            Price = products.Price,
        };
    }

    public ProductForReadDto CreateProduct(ProductForCreateDto dto)
    {
        
        // instanciar una objeto de clase Product
        // pasarle a ese objeto los valores de prodductforcreatedto
        // pasarle por parametro ese objeto al addproduct()

        Product product = new Product()
        {
            Name = dto.Name,
            Price = dto.Price
        };
        Product productCreated = _repository.AddProduct(product);
        
        // instanciar un objeto de la clase ProductForReadDto, pasarle
        // los valores de newproduct y finalmente retornarlo
        ProductForReadDto response = new ProductForReadDto
        {
            Id = productCreated.Id,
            Name = productCreated.Name,
            Price = productCreated.Price
        };
        
         return response;
    }

    public void UpdateProduct(int id, ProductForUpdateDto dto)
    {
        var product = _repository.GetProductById(id);
        if (product is null)
        {
            return;
        }
        
        product.Name = dto.Name;
        product.Price = dto.Price;
        _repository.UpdateProduct(product);

    }

    public void DeleteProduct(int id)
    {
        var product = _repository.GetProductById(id);
        if (product is null)
        {
            return;
        }
        
        _repository.DeleteProduct(product);
    }
    public bool NameExists(string name)
    {
        List<Product> products = _repository.GetAllProducts();
        return products.Any(p => p.Name.ToLower() == name.ToLower());
    }

    public List<ProductForReadDto> SearchProductByName(string name)
    {
        List<Product> products = _repository.SearchProductsByName(name);

        List<ProductForReadDto> result = new List<ProductForReadDto>();

        foreach (Product product in products)
        {
            result.Add(new ProductForReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            });
        }

        return result;
    }
    
    public ProductStatsDto GetStats()
    {
        List<Product> products = _repository.GetAllProducts();

        if (products.Count == 0)
        {
            throw new InvalidOperationException("No hay productos.");
        }

        ProductStatsDto result = new ProductStatsDto
        {
            Total = products.Count,
            AveragePrice = products.Average(x => x.Price),
            MostExpensiveName = products
                .OrderByDescending(x => x.Price)
                .First()
                .Name
        };

        return result;
    }
    
}