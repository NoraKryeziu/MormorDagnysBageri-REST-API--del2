using mormordagnysbageri_del1_api.ViewModel.Address;
using mormordagnysbageri_del1_api.ViewModel.Customer;
using mormordagnysbageri_del1_api.ViewModel.SalesOrder;

namespace mormordagnysbageri_del1_api.ViewModel;

public class CustomerViewModel:CustomerBaseViewModel
{
    public IList<AddressViewModel> Addresses { get; set; }
    public IList<SalesOrderViewModel> SalesOrders { get; set; }
    
}
