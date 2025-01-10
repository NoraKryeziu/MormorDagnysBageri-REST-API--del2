namespace mormordagnysbageri_del1_api.Entities;

public class Supplier
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public string Address { get; set; }
    public string ContactPerson { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public IList<SupplierProduct>SupplierProducts { get; set; }
 
}
