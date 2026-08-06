using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoya.Inventory.Domain.DTO
{
    public class InvoiceDto
    {
        public string Id { get; set; }
        public string InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public string PaymentMode { get; set; }
        public string? ExhibitionId { get; set; }
        public string Status { get; set; }

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

    public class ReturnOrderDTO
    {
            public int Quantity { get; set; }
            public string ProductId { get; set; }
            public string ProductSize{ get; set; }
    }

    public class DashboardDTO
    {
        public int TotalProducts { get; set; }
        public int TotalSold { get; set; }
        public decimal Revenue { get; set; }
        public decimal Profit { get; set; }
        public decimal TotalExpense { get; set; }
        public int DamageCount { get; set; }
        public decimal TotalDamage { get; set; }
    }
    public class HotSellingProductDTO
    {
        public string ProductName { get; set; }
        public string Code { get; set; }
        public decimal Revenue { get; set; }
    }
    public class DamageProductDTO
    {
        public int TotalProducts { get; set; }

        public int TotalSold { get; set; }

        public decimal Revenue { get; set; }

        public decimal Profit { get; set; }

        public decimal Expense { get; set; }

        public int TotalDamagedItems { get; set; }

        public decimal TotalDamageCost { get; set; }
    }
    public class DamagedProductDTO
    {
        public string Id { get; set; } = string.Empty;

        public string ProductId { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public string Size { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal BuyingPrice { get; set; }

        public decimal TotalLoss => BuyingPrice * Quantity;

        public string? Reason { get; set; }

        public string? Remarks { get; set; }

        public string AdjustmentType { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
