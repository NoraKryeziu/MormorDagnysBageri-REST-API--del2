using Microsoft.EntityFrameworkCore;
using mormordagnysbageri_del1_api.Data;
using mormordagnysbageri_del1_api.Entities;
using mormordagnysbageri_del1_api.Interfaces;
using mormordagnysbageri_del1_api.ViewModel.OrderItem;

namespace mormordagnysbageri_del1_api.Repositories;

public class OrderItemRepository(DataContext context) : IOrderItemRepository
{
    private readonly DataContext _context = context;

    public async Task<OrderItem> Add(OrderItemPostViewModel model)
    {
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == model.ProductId);

            if (product == null)
            {
                throw new MDBException($"Produkten med id {model.ProductId} finns inte.");
            }
            var orderItem = new OrderItem
            {
                OrderId = model.OrderId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                Price = model.Price
            };
            await _context.OrderItems.AddAsync(orderItem);

            return orderItem;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
