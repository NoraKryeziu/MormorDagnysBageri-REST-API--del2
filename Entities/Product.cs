namespace mormordagnysbageri_del1_api.Entities;

public class Product
{
    public int Id { get; set; }
    public string ItemNumber { get; set; }
    public string Name { get; set; }
    public double PricePerUnit { get; set; }
    public double Weight { get; set; }
    public int QuantityPerPack { get; set; }
    public DateTime ExpireDate { get; set; }
    public DateTime ManufactureDate { get; set; }
}
