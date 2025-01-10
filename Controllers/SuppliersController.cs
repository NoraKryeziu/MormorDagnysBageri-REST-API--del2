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
        .Include(sp => sp.SupplierProducts)
        .Select(supplier => new
        {
            supplier.SupplierName,
            supplier.ContactPerson,
            supplier.Email,
            supplier.Phone,
            Products = supplier.SupplierProducts
                .Select(supplierProduct => new
                {
                    supplierProduct.Product.ProductName,
                    supplierProduct.ItemNumber,
                    supplierProduct.Price
                })
        })
        .ToListAsync();
        return Ok(new { success = true, statusCode = 200, data = supplier });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindSupplier(int id)
    {
        var supplier = await _context.Suppliers
        .Where(s => s.SupplierId == id)
        .Include(sp => sp.SupplierProducts)
        .Select(supplier => new
        {
            supplier.SupplierName,
            supplier.ContactPerson,
            supplier.Email,
            supplier.Phone,
            Products = supplier.SupplierProducts
                .Select(supplierProduct => new
                {
                    supplierProduct.Product.ProductName,
                    supplierProduct.ItemNumber,
                    supplierProduct.Price
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
