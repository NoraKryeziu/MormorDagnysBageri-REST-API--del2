using mormordagnysbageri_del1_api.ViewModel.Customer;
using mormordagnysbageri_del1_api.ViewModel.OrderItem;

namespace mormordagnysbageri_del1_api.ViewModel.SalesOrder;

public class SalesOrderViewModel: SalesOrderBaseViewModel
{

    public IList<OrderItemViewModel> Items { get; set; }
}
