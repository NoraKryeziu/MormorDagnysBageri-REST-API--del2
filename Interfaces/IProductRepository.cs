using mormordagnysbageri_del1_api.ViewModel;
using mormordagnysbageri_del1_api.ViewModel.Product;

namespace mormordagnysbageri_del1_api.Interfaces;

public interface IProductRepository
{
    public Task<bool> Add(ProductViewModel model);
    public Task<ProductViewModel> Find(int id);
    public Task<IList<ProductsViewModel>> List();
    public Task<bool> Update (int id, ProductViewModel model);
}
