using Microsoft.EntityFrameworkCore;
using mormordagnysbageri_del1_api.Data;
using mormordagnysbageri_del1_api.Entities;
using mormordagnysbageri_del1_api.Interfaces;
using mormordagnysbageri_del1_api.ViewModel.OrderItem;
using mormordagnysbageri_del1_api.ViewModel.SalesOrder;

namespace mormordagnysbageri_del1_api.Repositories;

public class OrderRepository(DataContext context, IOrderItemRepository orderItemRepo) : IOrderRepository
{
    private readonly DataContext _context = context;
    private readonly IOrderItemRepository _orderItemRepo = orderItemRepo;

    public async Task<bool> Add(SalesOrderPostViewModel model)
    {

        try
        {
            var order = new SalesOrder
            {
                CustomerId = model.CustomerId,
                OrderDate = DateTime.Now.Date
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();


            foreach (var item in model.OrderItems)
            {
                item.OrderId = order.OrderId;
                await _orderItemRepo.Add(item);

            }
            
            return true;
        }
        catch (Exception ex)
        {

            throw new Exception($"Ett fel uppstod{ex.Message}");
        }
    }



    public async Task<SalesOrderViewModel> Find(int id)
    {
        var order = await _context.Orders
        .Where(c => c.OrderId == id)
        .Include(c => c.Customer)
        .Include(c => c.OrderItems)
            .ThenInclude(c => c.Product)
        .SingleOrDefaultAsync();

        if (order is null)
        {
            throw new MDBException($"Finns ingen order med id {id}");
        }

        var view = new SalesOrderViewModel
        {
            OrderId = order.OrderId,
            CustomerName = order.Customer.Name,
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
        view.Items = items;

        return view;
    }

    public async Task<IList<SalesOrdersViewModel>> Find(DateTime orderDate)
    {
        var orders = await _context.Orders
        .Where(c => c.OrderDate == orderDate)
        .Include(c => c.Customer)
        .Include(c => c.OrderItems)
            .ThenInclude(c => c.Product)
        .ToListAsync();

        if (!orders.Any())
        {
            throw new MDBException($"Finns ingen order med orderdatum {orderDate}");
        }


        IList<SalesOrdersViewModel> response = [];


        foreach (var order in orders)
        {

            var view = new SalesOrdersViewModel
            {
                OrderId = order.OrderId,
                CustomerName = order.Customer.Name,
                OrderDate = order.OrderDate
            };
            response.Add(view);

        }
        return response;

    }

    public async Task<IList<SalesOrdersViewModel>> List()
    {
        try
        {
            var orders = await _context.Orders
            .Include(c => c.Customer)
            .ToListAsync();

            IList<SalesOrdersViewModel> response = [];

            foreach (var order in orders)
            {
                var view = new SalesOrdersViewModel
                {
                    OrderId = order.OrderId,
                    CustomerName = order.Customer.Name,
                    OrderDate = order.OrderDate
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
}
