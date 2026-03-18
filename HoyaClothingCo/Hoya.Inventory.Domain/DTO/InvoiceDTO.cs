using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoya.Inventory.Domain.DTO
{
    public class InvoiceDto
    {

        public string InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public string PaymentMode { get; set; }
        public string? ExhibitionId { get; set; }

        public List<InvoiceItemDto> Items { get; set; } = new();
    }

    public class InvoiceItemDto
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

    }
}
