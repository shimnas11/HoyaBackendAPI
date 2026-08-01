using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoya.Inventory.Domain.Entities.Masters
{
    public class StockPurchase :AggregateRoot
    {
        public string PurchaseNo { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public string vendorName { get; set; }
        public string PhoneNumber { get; set; }
        public int TotalItems { get; set; }
        public int TotalQuantity { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string? Remarks { get; set; }
    }

    public class Category : AggregateRoot
    {
        public string Name { get; set; }
        public Category(string name)
        {
            Name = name;
        }
    }
    public class MaterialType : AggregateRoot
    {
        public string Name { get; set; }

        public MaterialType(string name)
        {
            Name = name;
        }

    }

}
