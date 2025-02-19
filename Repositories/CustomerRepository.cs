using Microsoft.EntityFrameworkCore;
using mormordagnysbageri_del1_api.Data;
using mormordagnysbageri_del1_api.Entities;
using mormordagnysbageri_del1_api.Interfaces;
using mormordagnysbageri_del1_api.ViewModel;
using mormordagnysbageri_del1_api.ViewModel.Address;
using mormordagnysbageri_del1_api.ViewModel.Customer;
using mormordagnysbageri_del1_api.ViewModel.OrderItem;
using mormordagnysbageri_del1_api.ViewModel.SalesOrder;

namespace mormordagnysbageri_del1_api;

public class CustomerRepository(DataContext context, IAddressRepository repo) : ICustomerRespository
{
    private readonly DataContext _context = context;
    private readonly IAddressRepository _repo = repo;


    public async Task<bool> Add(CustomerPostViewModel model)
    {

        try
        {
            if(await _context.Customers.FirstOrDefaultAsync(c => c.Email.ToLower().Trim() == model.Email.ToLower().Trim() )!= null)
            {
                throw new MDBException("Kunden finns redan");
            }

            var customer = new Customer
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                ContactPerson = model.ContactPerson
            };

            await _context.AddAsync(customer);

            foreach(var a in model.Addresses)
            {
                var address = await _repo.Add(a);
                await _context.CustomerAddresses.AddAsync(new CustomerAddress
                {
                    Address = address,
                    Customer = customer
                });
            }
            return true;
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }
    }

    public async Task<CustomerViewModel> Find(int id)
    {
        try
        {
            var customer = await _context.Customers
            .Where(c => c.Id == id)
            .Include(c => c.CustomerAddresses)
                .ThenInclude(c => c.Address)
                .ThenInclude(c => c.PostalAddress)
            .Include(c => c.CustomerAddresses)
                .ThenInclude(c => c.Address)
                .ThenInclude(c => c.AddressType)
            .Include(c => c.SalesOrders)
                .ThenInclude(c => c.OrderItems)
                .ThenInclude(c => c.Product)
            .SingleOrDefaultAsync();

            if (customer is null)
            {
                throw new MDBException($"Finns ingen kund med id {id}");
            }

            var view = new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                ContactPerson = customer.ContactPerson
            };

            IList<AddressViewModel> addresses = [];

            foreach (var address in customer.CustomerAddresses)
            {
                var addressView = new AddressViewModel
                {
                    AddressLine = address.Address.AddressLine,
                    PostalCode = address.Address.PostalAddress.PostalCode,
                    City = address.Address.PostalAddress.City,
                    AddressType = address.Address.AddressType.Value
                };
                addresses.Add(addressView);
            }
            view.Addresses = addresses;

            IList<SalesOrderViewModel> orders = [];


            foreach (var order in customer.SalesOrders)
            {

                var orderView = new SalesOrderViewModel
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate
                };

                IList<OrderItemViewModel> items = [];

                foreach (var item in order.OrderItems)
                {
                    var itemView = new OrderItemViewModel
                    {
                        ProductName = item.Product.Name,
                        Quantity = item.Quantity,
                        Price = item.Price
                    };
                    items.Add(itemView);
                }
                orderView.Items = items;

                orders.Add(orderView);
            }
            view.SalesOrders = orders;

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

    public async Task<IList<CustomersViewModel>> List()
    {
        try
        {
            var customers = await _context.Customers.ToListAsync();

            IList<CustomersViewModel> response = [];

            foreach (var customer in customers)
            {
                var view = new CustomersViewModel
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    ContactPerson = customer.ContactPerson
                };
                response.Add(view);
            }
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception($"Ett fel uppstod{ex.Message}" );
        }

    }
}
