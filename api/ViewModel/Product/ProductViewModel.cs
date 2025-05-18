namespace mormordagnysbageri_del1_api.ViewModel.Product;

public class ProductViewModel: ProductsViewModel
{
    public double Weight { get; set; }
    public DateTime ExpireDate { get; set; }
    public DateTime ManufactureDate { get; set; }
    public IList<CustomersViewModel> Customers { get; set; }
}
