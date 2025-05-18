using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mormordagnysbageri_del1_api.Data;
using mormordagnysbageri_del1_api.Entities;

namespace mormordagnysbageri_del1_api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class SuppliersController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpGet()]
    public async Task<ActionResult> ListAllSuppliers()
    {
        var supplier = await _context.Suppliers
        .Include(sp => sp.SupplierIngredients)
        .Select(supplier => new
        {
            supplier.Name,
            supplier.ContactPerson,
            supplier.Email,
            supplier.Phone,
            Products = supplier.SupplierIngredients
                .Select(c => new
                {
                    c.Ingredient.Name,
                    c.ItemNumber,
                    c.Price
                })
        })
        .ToListAsync();
        return Ok(new { success = true, statusCode = 200, data = supplier });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindSupplier(int id)
    {
        var supplier = await _context.Suppliers
        .Where(s => s.Id == id)
        .Include(sp => sp.SupplierIngredients)
        .Select(supplier => new
        {
            supplier.Name,
            supplier.ContactPerson,
            supplier.Email,
            supplier.Phone,
            Ingredient = supplier.SupplierIngredients
                .Select(c => new
                {
                    c.Ingredient.Name,
                    c.ItemNumber,
                    c.Price
                })
        })
        .SingleOrDefaultAsync();

        if (supplier is null)
        {
            return NotFound(new { success = false, statusCode = 404, message = $"Tyvärr, vi kunde inte hitta någon leverantör med id:{id} " });
        }
        return Ok(new { success = true, StatusCode = 200, data = supplier });

    }
    

}
