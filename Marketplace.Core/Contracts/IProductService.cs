using Marketplace.Core.Models;

namespace Marketplace.Core.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListViewModel>> GetProducts();
        Task<IEnumerable<ProductListViewModel>> GetProductsWithZeroQuantity();
        Task<ProductToEditImagesViewModel> GetProductToEditImages(string id);
        Task<ProductEditViewModel> GetProductToEdit(string id);
        Task<bool> ProductToEdit(ProductEditViewModel model);
        Task<bool> AddImage(string id, string imagePath);
        Task<bool> DeleteProduct(string id);
        Task<bool> DeleteImage(string id, string imagePath);
        Task<bool> AddProduct(AddProductViewModel model);
    }
}
