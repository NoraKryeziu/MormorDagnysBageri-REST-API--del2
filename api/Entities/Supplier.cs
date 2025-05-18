namespace mormordagnysbageri_del1_api.Entities;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ContactPerson { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public IList<SupplierIngredient>SupplierIngredients { get; set; }
    public IList<SupplierAddress> SupplierAddresses { get; set; }
 
}
