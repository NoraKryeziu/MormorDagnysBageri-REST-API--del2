using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mormordagnysbageri_del1_api.Data;
using mormordagnysbageri_del1_api.Entities;
using mormordagnysbageri_del1_api.ViewModel;




namespace mormordagnysbageri_del1_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpGet()]
    public async Task<ActionResult> ListAllIngredients
    ()
    {
        var ingredients = await _context.Ingredients
        .Include(c => c.SupplierIngredients)
        .Select(c => new
        {
            c.Name,
            SupplierProducts = c.SupplierIngredients
                .Select(c => new
                {
                    c.ItemNumber,
                    c.Price,
                    c.Supplier.Name
                })
        })
        .ToListAsync();
        return Ok(new { success = true, statusCode = 200, data = ingredients });
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> Find(int id)
    {
        var ingredient = await _context.Ingredients
        .Where(c => c.IngredientId == id)
        .Include(c => c.SupplierIngredients)
        .Select(c => new
        {
            c.Name,
            SupplierProduct = c.SupplierIngredients
                .Select(c => new
                {
                    c.ItemNumber,
                    c.Price,
                    c.Supplier.Name
                })
        })
        .SingleOrDefaultAsync();

        if (ingredient is null)
        {
            return NotFound(new { success = false, statusCode = 404, message = $"Tyvärr, vi kunde inte hitta någon produkt med artikelnummer:{id} " });
        }
        return Ok(new { success = true, StatusCode = 200, data = ingredient });
    }

    [HttpPost()]
    public async Task<ActionResult> Add(IngredientPostViewModel model)
    {
        var ingredient = await _context.Ingredients.FirstOrDefaultAsync(c => c.ItemNumber == model.ItemNumber);
        var supplier = await _context.Suppliers.FirstOrDefaultAsync(c => c.Id == model.SupplierId);

        if (ingredient is not null || supplier is null)
        {
            return NotFound(new { success = false, statusCode = 404, message = $"Tyvärr gick det inte att lägga till ingrediensen: {ingredient.Name}" });
        }

        var newIngredient = new Ingredient
        {
            ItemNumber = model.ItemNumber,
            Name = model.Name,
            Price = model.Price
        };

        try
        {
            await _context.Ingredients.AddAsync(newIngredient);
            await _context.SaveChangesAsync();

            var supplierIngredient = new SupplierIngredient
            {
                SupplierId = model.SupplierId,
                IngredientId = newIngredient.IngredientId,
                ItemNumber = model.ItemNumber,
                Price = model.Price
            };

            await _context.SupplierIngredients.AddAsync(supplierIngredient);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = $"Ingrediensen {model.Name} har lagts till hos leverantören {supplier.Name}." });
        }
        catch (Exception ex)
        {

            return StatusCode(500, ex.Message);

        }
    }
    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateIngredientPrice(int id, [FromQuery] decimal price, [FromQuery] int supplierId)
    {
        var ingredient = await _context.Ingredients.FirstOrDefaultAsync(p => p.IngredientId == id);
        var supplierIngredient = await _context.SupplierIngredients.FirstOrDefaultAsync(sp => sp.SupplierId == supplierId);

        if (ingredient is null || supplierIngredient is null)
        {
            return BadRequest(new { success = false, message = "Tyvärr, det gick inte att uppdatera priset" });
        }

        supplierIngredient.Price = price;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

        return NoContent();  
    }

}