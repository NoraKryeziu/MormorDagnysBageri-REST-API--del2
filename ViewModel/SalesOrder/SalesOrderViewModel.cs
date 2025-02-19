using mormordagnysbageri_del1_api.ViewModel.OrderItem;

namespace mormordagnysbageri_del1_api.ViewModel.SalesOrder;

public class SalesOrderViewModel
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public IList<OrderItemViewModel> Items { get; set; }


}
