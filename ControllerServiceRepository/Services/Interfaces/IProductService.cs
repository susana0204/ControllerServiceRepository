using ControllerServiceRepository.Models.DTOs.Requests;
using ControllerServiceRepository.Models.DTOs.Responses;

namespace ControllerServiceRepository.Services.Interfaces;

public interface IProductService
{
  List<ProductForReadDto> GetAllProducts();
 ProductForReadDto? GetProductById(int id);
 ProductForReadDto CreateProduct(ProductForCreateDto dto);
 void UpdateProduct(int id, ProductForUpdateDto dto);
  void DeleteProduct(int id);
}