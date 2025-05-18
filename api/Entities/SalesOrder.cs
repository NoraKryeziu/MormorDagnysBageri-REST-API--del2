using System.ComponentModel.DataAnnotations;

namespace mormordagnysbageri_del1_api.Entities;

public class SalesOrder
{
    [Key]
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public Customer Customer  { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    

}
