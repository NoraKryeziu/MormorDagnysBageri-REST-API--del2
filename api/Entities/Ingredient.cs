using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mormordagnysbageri_del1_api.Entities;

public class Ingredient
{

    public int IngredientId { get; set; }
    public string ItemNumber { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public IList<SupplierIngredient>SupplierIngredients { get; set; }
 
}
