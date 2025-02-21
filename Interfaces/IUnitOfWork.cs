namespace mormordagnysbageri_del1_api.Interfaces;

public interface IUnitOfWork
{
    ICustomerRespository CustomerRepository { get; }
    IAddressRepository AddressRepository { get; }
    IProductRepository ProductRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderItemRepository OrderItemRepository { get; }

    Task<bool> Complete();
    bool HasChanges();
}
