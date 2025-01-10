using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mormordagnysbageri_del1_api.Data;
using mormordagnysbageri_del1_api.Entities;
using mormordagnysbageri_del1_api.ViewModel;




namespace mormordagnysbageri_del1_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpGet()]
    public async Task<ActionResult> ListAllProducts()
    {
        var products = await _context.Products
        .Include(p => p.SupplierProducts)
        .Select(product => new
        {
            product.ProductName,
            SupplierProducts = product.SupplierProducts
                .Select(supplierProduct => new
                {
                    supplierProduct.ItemNumber,
                    supplierProduct.Price,
                    supplierProduct.Supplier.SupplierName
                })
        })
        .ToListAsync();
        return Ok(new { success = true, statusCode = 200, data = products });
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> FindProduct(int id)
    {
        var product = await _context.Products
        .Where(p => p.ProductId == id)
        .Include(x => x.SupplierProducts)
        .Select(product => new
        {
            product.ProductName,
            SupplierProduct = product.SupplierProducts
                .Select(supplierProduct => new
                {
                    supplierProduct.ItemNumber,
                    supplierProduct.Price,
                    supplierProduct.Supplier.SupplierName
                })
        })
        .SingleOrDefaultAsync();

        if (product is null)
        {
            return NotFound(new { success = false, statusCode = 404, message = $"Tyvärr, vi kunde inte hitta någon produkt med artikelnummer:{id} " });
        }
        return Ok(new { success = true, StatusCode = 200, data = product });
    }

    [HttpPost()]
    public async Task<ActionResult> AddProduct(ProductPostViewModel model)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ItemNumber == model.ItemNumber);
        var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == model.SupplierId);

        if (product is not null || supplier is null)
        {
            return NotFound(new { success = false, statusCode = 404, message = $"Tyvärr gick det inte att lägga till produkten: {product.ProductName}" });
        }

        var newProduct = new Product
        {
            ItemNumber = model.ItemNumber,
            ProductName = model.ProductName,
            Price = model.Price
        };

        try
        {
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            var supplierProduct = new SupplierProduct
            {
                SupplierId = model.SupplierId,
                ProductId = newProduct.ProductId,
                ItemNumber = model.ItemNumber,
                Price = model.Price
            };

            await _context.SupplierProducts.AddAsync(supplierProduct);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = $"Produkten {model.ProductName} har lagts till hos leverantören {supplier.SupplierName}." });
        }
        catch (Exception ex)
        {

            return StatusCode(500, ex.Message);

        }
    }
    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateProductPrice(int id, [FromQuery] decimal price, [FromQuery] int supplierId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        var supplierProduct = await _context.SupplierProducts.FirstOrDefaultAsync(sp => sp.SupplierId == supplierId);

        if (product is null || supplierProduct is null)
        {
            return BadRequest(new { success = false, message = "Tyvärr, det gick inte att uppdatera priset" });
        }

        supplierProduct.Price = price;

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