using mormordagnysbageri_del1_api.Entities;

namespace mormordagnysbageri_del1_api.ViewModel;

public class IngredientPostViewModel
{
    public string ItemNumber { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int SupplierId { get; set; }

    public Supplier Supplier { get; set; }
}
