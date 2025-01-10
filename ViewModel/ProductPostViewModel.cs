using mormordagnysbageri_del1_api.Entities;

namespace mormordagnysbageri_del1_api.ViewModel;

public class ProductPostViewModel
{
    public string ItemNumber { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int SupplierId { get; set; }

    public Supplier Supplier { get; set; }
}
