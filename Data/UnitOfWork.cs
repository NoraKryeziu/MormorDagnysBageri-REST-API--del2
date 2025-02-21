using mormordagnysbageri_del1_api.Interfaces;
using mormordagnysbageri_del1_api.Repositories;

namespace mormordagnysbageri_del1_api.Data;

public class UnitOfWork(DataContext context, IAddressRepository repo, IOrderItemRepository orderItemRepo):IUnitOfWork
{
    private readonly DataContext _context = context;
    private readonly IAddressRepository _repo = repo;
    private readonly IOrderItemRepository _orderItemRepo = orderItemRepo;
    public ICustomerRespository CustomerRepository => new CustomerRepository(_context, _repo);
    public IAddressRepository AddressRepository => new AddressRepository(_context);

    public IProductRepository ProductRepository => new ProductRepository(_context);
    public IOrderRepository OrderRepository => new OrderRepository(_context, _orderItemRepo);
    public IOrderItemRepository OrderItemRepository => new OrderItemRepository(_context);

    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }
}
