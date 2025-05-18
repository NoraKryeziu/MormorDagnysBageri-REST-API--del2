using mormordagnysbageri_del1_api.ViewModel.Address;

namespace mormordagnysbageri_del1_api.ViewModel.Customer;

public class CustomerPostViewModel: CustomerBaseViewModel
{
    public IList<AddressPostViewModel> Addresses { get; set; }
}
