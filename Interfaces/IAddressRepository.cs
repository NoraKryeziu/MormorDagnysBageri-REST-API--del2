using mormordagnysbageri_del1_api.Entities;
using mormordagnysbageri_del1_api.ViewModel.Address;

namespace mormordagnysbageri_del1_api.Interfaces;

public interface IAddressRepository
{
    public Task<Address> Add (AddressPostViewModel model);
    public Task<bool> Add(string type);
}
