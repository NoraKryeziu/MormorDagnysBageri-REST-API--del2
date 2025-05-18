using Microsoft.EntityFrameworkCore;
using mormordagnysbageri_del1_api.Entities;

namespace mormordagnysbageri_del1_api.Data;

public class DataContext : DbContext
{
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierIngredient> SupplierIngredients { get; set; }
    public DbSet<AddressType> AddressTypes { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<PostalAddress> PostalAddresses { get; set; }
    public DbSet<CustomerAddress> CustomerAddresses { get; set; }
    public DbSet<SupplierAddress> SupplierAddresses { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<SalesOrder> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderItem>().HasKey(oi => new { oi.OrderId, oi.ProductId });
        modelBuilder.Entity<SupplierIngredient>().HasKey(sp => new { sp.IngredientId, sp.SupplierId });
        modelBuilder.Entity<CustomerAddress>().HasKey(c => new { c.AddressId, c.CustomerId });
        modelBuilder.Entity<SupplierAddress>().HasKey(s => new { s.AddressId, s.SupplierId });
    }
}
