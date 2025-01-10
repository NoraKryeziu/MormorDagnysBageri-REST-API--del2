using Microsoft.EntityFrameworkCore;
using mormordagnysbageri_del1_api.Entities;

namespace mormordagnysbageri_del1_api.Data;

public class DataContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierProduct> SupplierProducts { get; set; }
    
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupplierProduct>().HasKey(sp => new {sp.ProductId, sp.SupplierId});
    }
}
