namespace mormordagnysbageri_del1_api.Entities;

public class OrderItem
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Product Product { get; set; }
    public SalesOrder Order { get; set; }
}
