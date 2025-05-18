using Microsoft.EntityFrameworkCore;
using mormordagnysbageri_del1_api.Data;
using mormordagnysbageri_del1_api.Entities;
using mormordagnysbageri_del1_api.Interfaces;
using mormordagnysbageri_del1_api.ViewModel;
using mormordagnysbageri_del1_api.ViewModel.Product;
using mormordagnysbageri_del1_api.ViewModel.SalesOrder;

namespace mormordagnysbageri_del1_api.Repositories;

public class ProductRepository(DataContext context) : IProductRepository
{
    private readonly DataContext _context = context;

    public async Task<bool> Add(ProductViewModel model)
    {
        try
        {
            if (await _context.Products.FirstOrDefaultAsync(c => c.ItemNumber.Replace(" ", "").ToLower().Trim() == model.ItemNumber.Replace(" ", "").ToLower().Trim()) != null)
            {
                throw new MDBException("Produkten finns redan");
            }
            var product = new Product
            {
                ItemNumber = model.ItemNumber,
                Name = model.Name,
                PricePerUnit = model.PricePerUnit,
                Weight = model.Weight,
                QuantityPerPack = model.QuantityPerPack,
                ExpireDate = model.ExpireDate,
                ManufactureDate = model.ManufactureDate
            };
            await _context.AddAsync(product);

            return true;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);

        }
    }

    public async Task<ProductViewModel> Find(int id)
    {
        try
        {
            var product = await _context.Products
            .Where(c => c.Id == id)
            .Include(c => c.OrderItems)
                .ThenInclude(c => c.Order)
                    .ThenInclude(c => c.Customer)
            .SingleOrDefaultAsync();

            if (product is null)
            {
                throw new MDBException($"Finns ingen produkt med id {id}");
            }


            var view = new ProductViewModel
            {
                Id = product.Id,
                ItemNumber = product.ItemNumber,
                Name = product.Name,
                PricePerUnit = product.PricePerUnit,
                Weight = product.Weight,
                QuantityPerPack = product.QuantityPerPack,
                ExpireDate = product.ExpireDate,
                ManufactureDate = product.ManufactureDate
            };
            IList<CustomersViewModel> customers = [];

            foreach (var orderItem in product.OrderItems)
            {
                var customer = orderItem.Order.Customer;

                var exists = customers.FirstOrDefault(c => c.Id == customer.Id);
                if(exists is null) 
                {
                    var customerView = new CustomersViewModel
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                        Email = customer.Email,
                        Phone = customer.Phone,
                        ContactPerson = customer.ContactPerson
                    };
                    customers.Add(customerView);
                }
            }
            view.Customers = customers;

            return view;
        }
        catch (MDBException ex)
        {
            throw new Exception(ex.Message);
        }
        catch (Exception ex)
        {

            throw new Exception($"Ett fel uppstod. {ex.Message}");
        }
    }

    public async Task<IList<ProductsViewModel>> List()
    {
        try
        {
            var products = await _context.Products.ToListAsync();

            IList<ProductsViewModel> response = [];

            foreach (var product in products)
            {
                var view = new ProductsViewModel
                {
                    Id = product.Id,
                    ItemNumber = product.ItemNumber,
                    Name = product.Name,
                    PricePerUnit = product.PricePerUnit,
                    QuantityPerPack = product.QuantityPerPack
                };
                response.Add(view);
            }
            return response;
        }
        catch (Exception ex)
        {

            throw new Exception($"Ett fel uppstod{ex.Message}");

        }

    }

    public async Task<bool> Update(int id, ProductViewModel model)
    {
        try
        {
            var result = await _context.Products
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync();

            result.PricePerUnit = model.PricePerUnit;

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
