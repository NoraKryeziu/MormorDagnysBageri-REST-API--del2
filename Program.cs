using Microsoft.Extensions.Options;
using mormordagnysbageri_del1_api.Data;
using Microsoft.EntityFrameworkCore;
using mormordagnysbageri_del1_api;
using mormordagnysbageri_del1_api.Interfaces;
using mormordagnysbageri_del1_api.Repositories;

var builder = WebApplication.CreateBuilder(args);

var serverVersion = new MySqlServerVersion(new Version(9, 1, 0));

builder.Services.AddDbContext<DataContext>(options =>
{
    //options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
    options.UseMySql(builder.Configuration.GetConnectionString("MySQL"), serverVersion);
    
});

builder.Services.AddScoped<ICustomerRespository, CustomerRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();

var app = builder.Build();

using var scope = app.Services.CreateScope();

try
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();

    await context.Database.MigrateAsync();

    await Seed.LoadAddressTypes(context);
    await Seed.LoadPostalAddresses(context);
    await Seed.LoadAddresses(context);
    await Seed.LoadIngredients(context);
    await Seed.LoadSuppliers(context);
    await Seed.LoadSupplierAddresses(context);
    await Seed.LoadSupplierIngredients(context);
    await Seed.LoadCustomers(context);
    await Seed.LoadCustomerAddresses(context);
    await Seed.LoadProducts(context);
    await Seed.LoadSalesOrders(context);
    await Seed.LoadOrderItems(context);
   
}
catch (Exception ex)
{
    Console.WriteLine("{0}", ex.Message);
    throw;
}


app.MapControllers();

app.Run();

