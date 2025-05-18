using mormordagnysbageri_del1_api.ViewModel.SalesOrder;

namespace mormordagnysbageri_del1_api.Interfaces;

public interface IOrderRepository
{
    public Task<bool> Add(SalesOrderPostViewModel model);
    public Task<SalesOrderViewModel> Find(int id);
    public Task<IList<SalesOrdersViewModel>> Find(DateTime orderDate);
    public Task<IList<SalesOrdersViewModel>> List();
}
