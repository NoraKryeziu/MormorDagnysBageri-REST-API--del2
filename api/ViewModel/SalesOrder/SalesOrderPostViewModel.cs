using mormordagnysbageri_del1_api.ViewModel.OrderItem;

namespace mormordagnysbageri_del1_api.ViewModel.SalesOrder;

public class SalesOrderPostViewModel: SalesOrderViewModel
{
    public int CustomerId { get; set; }
    public IList<OrderItemPostViewModel> OrderItems { get; set; }

}
