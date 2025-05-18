using System.Text.Json;
using mormordagnysbageri_del1_api.Entities;

namespace mormordagnysbageri_del1_api.Data;
public static class Seed
{
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };
    public static async Task LoadIngredients(DataContext context)
    {

        if (context.Ingredients.Any()) return;

        var json = File.ReadAllText("Data/json/ingredients.json");

        var ingredients = JsonSerializer.Deserialize<List<Ingredient>>(json, options);

        if (ingredients is not null && ingredients.Count > 0)
        {
            await context.Ingredients.AddRangeAsync(ingredients);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadSuppliers(DataContext context)
    {
        if (context.Suppliers.Any()) return;

        var json = File.ReadAllText("Data/json/suppliers.json");

        var suppliers = JsonSerializer.Deserialize<List<Supplier>>(json, options);

        if (suppliers is not null && suppliers.Count > 0)
        {
            await context.Suppliers.AddRangeAsync(suppliers);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadSupplierIngredients(DataContext context)
    {

        if (context.SupplierIngredients.Any()) return;

        var json = File.ReadAllText("Data/json/supplieringredients.json");

        var supplierIngredients = JsonSerializer.Deserialize<List<SupplierIngredient>>(json, options);

        if (supplierIngredients is not null && supplierIngredients.Count > 0)
        {
            await context.SupplierIngredients.AddRangeAsync(supplierIngredients);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadAddressTypes(DataContext context)
    {
        if (context.AddressTypes.Any()) return;

        var json = await File.ReadAllTextAsync("Data/json/addressTypes.json");
        var types = JsonSerializer.Deserialize<List<AddressType>>(json, options);

        if (types is not null && types.Count > 0)
        {
            await context.AddressTypes.AddRangeAsync(types);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadAddresses(DataContext context)
    {
        if (context.Addresses.Any()) return;

        var json = await File.ReadAllTextAsync("Data/json/addresses.json");
        var address = JsonSerializer.Deserialize<List<Address>>(json, options);

        if (address is not null && address.Count > 0)
        {
            await context.Addresses.AddRangeAsync(address);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadPostalAddresses(DataContext context)
    {
        if (context.PostalAddresses.Any()) return;

        var json = await File.ReadAllTextAsync("Data/json/postalAddresses.json");
        var address = JsonSerializer.Deserialize<List<PostalAddress>>(json, options);

        if (address is not null && address.Count > 0)
        {
            await context.PostalAddresses.AddRangeAsync(address);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadSupplierAddresses(DataContext context)
    {
        if (context.SupplierAddresses.Any()) return;

        var json = await File.ReadAllTextAsync("Data/json/supplierAddresses.json");
        var address = JsonSerializer.Deserialize<List<SupplierAddress>>(json, options);

        if (address is not null && address.Count > 0)
        {
            await context.SupplierAddresses.AddRangeAsync(address);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadCustomers(DataContext context)
    {
        if (context.Customers.Any()) return;

        var json = await File.ReadAllTextAsync("Data/json/customers.json");
        var customer = JsonSerializer.Deserialize<List<Customer>>(json, options);

        if (customer is not null && customer.Count > 0)
        {
            await context.Customers.AddRangeAsync(customer);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadCustomerAddresses(DataContext context)
    {
        if (context.CustomerAddresses.Any()) return;

        var json = await File.ReadAllTextAsync("Data/json/customerAddresses.json");
        var address = JsonSerializer.Deserialize<List<CustomerAddress>>(json, options);

        if (address is not null && address.Count > 0)
        {
            await context.CustomerAddresses.AddRangeAsync(address);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadProducts(DataContext context)
    {
        if (context.Products.Any()) return;

        var json = await File.ReadAllTextAsync("Data/json/products.json");
        var product = JsonSerializer.Deserialize<List<Product>>(json, options);

        if (product is not null && product.Count > 0)
        {
            await context.Products.AddRangeAsync(product);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadSalesOrders(DataContext context)
    {
        if (context.Orders.Any()) return;

        var json = await File.ReadAllTextAsync("Data/json/orders.json");
        var order = JsonSerializer.Deserialize<List<SalesOrder>>(json, options);

        if (order is not null && order.Count > 0)
        {
            await context.Orders.AddRangeAsync(order);
            await context.SaveChangesAsync();
        }
    }
    public static async Task LoadOrderItems(DataContext context)
    {
        if (context.OrderItems.Any()) return;

        var json = await File.ReadAllTextAsync("Data/json/orderItems.json");
        var item = JsonSerializer.Deserialize<List<OrderItem>>(json, options);

        if (item is not null && item.Count > 0)
        {
            await context.OrderItems.AddRangeAsync(item);
            await context.SaveChangesAsync();
        }
    }

}
