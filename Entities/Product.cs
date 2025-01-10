namespace mormordagnysbageri_del1_api.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string ItemNumber { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public IList<SupplierProduct>SupplierProducts { get; set; }
 }
