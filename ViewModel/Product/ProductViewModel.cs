namespace mormordagnysbageri_del1_api.ViewModel.Product;

public class ProductViewModel: ProductsViewModel
{
    public int Id { get; set; }
    public double Weight { get; set; }
    public DateTime ExpireDate { get; set; }
    public DateTime ManufactureDate { get; set; }
    public IList<CustomersViewModel> Customers { get; set; }
}
