using Marketplace.Core.Models;

namespace Marketplace.Core.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListViewModel>> GetProducts();
        Task<ProductToEditViewModel> GetProductToEdit(string id);
        Task<bool> AddImage(string id, string imagePath);
        Task<bool> DeleteImage(string id, string imagePath);
        Task<bool> AddProduct(AddProductViewModel model);
    }
}
