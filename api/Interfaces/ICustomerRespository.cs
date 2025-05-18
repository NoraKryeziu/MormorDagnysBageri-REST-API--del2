using mormordagnysbageri_del1_api.ViewModel;
using mormordagnysbageri_del1_api.ViewModel.Customer;

namespace mormordagnysbageri_del1_api;

public interface ICustomerRespository
{
    public Task<IList<CustomersViewModel>>List();
    public Task<CustomerViewModel>Find(int id);
    public Task<bool> Add(CustomerPostViewModel model);
    public Task<bool> Update(int id, CustomerBaseViewModel model);
}
