using System.Text.Json.Serialization;

namespace mormordagnysbageri_del1_api.ViewModel.Address;

public class AddressPostViewModel
{
    public enum AddressTypeEnum
    {
        Delivery = 1,
        Invoice = 2,
        Distribution = 3
    }

        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AddressTypeEnum AddressType { get; set; }
    
}
