namespace mormordagnysbageri_del1_api.Entities;
public class SupplierIngredient
{
    public int IngredientId { get; set; }
    public int SupplierId { get; set; }
    public string ItemNumber { get; set; }
    public decimal Price { get; set; }

    public Ingredient Ingredient { get; set; }
    public Supplier Supplier { get; set; }
} 
