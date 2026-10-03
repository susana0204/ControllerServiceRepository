using ControllerServiceRepository.Entities;

namespace ControllerServiceRepository.Repositories.Interfaces;

public interface IProductRepository
{
   List<Product> GetAllProducts();
   Product? GetProductById(int id);
   Product AddProduct(Product product); 
   void UpdateProduct(Product product); 
   void DeleteProduct(Product product);

}