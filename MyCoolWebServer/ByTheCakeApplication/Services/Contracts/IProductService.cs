using MyCoolWebServer.ByTheCakeApplication.ViewModels.Products;
using System.Collections.Generic;

namespace MyCoolWebServer.ByTheCakeApplication.Services.Contracts
{
    public interface IProductService
    {
        void Create(string name, decimal price, string imageUrl);

        IEnumerable<ProductListingViewModel> All(string searchTerm = null);

        ProductDetailsViewModel Find(int id);

        bool Exists(int id);

        IEnumerable<ProductInCartViewModel> FindProductsInCart(IEnumerable<int> Ids);
    }
}
