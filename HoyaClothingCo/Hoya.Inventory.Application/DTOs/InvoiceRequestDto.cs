using System.Runtime.Serialization;

namespace Hoya.Inventory.Application.DTOs
{
    public class InvoiceRequestDto
    {
        public List<InvoiceProductDto> Products { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public string   PaymentMode { get; set; }

        //[IgnoreDataMember]
        public string? ExhibitionId { get; set; }
    }


}
