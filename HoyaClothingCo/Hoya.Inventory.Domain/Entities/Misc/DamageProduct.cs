using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoya.Inventory.Domain.Entities.Misc
{
    public class DamagedProduct
    {
        public DamagedProduct()
        {
            Id = Guid.NewGuid().ToString();
        }

        public void SetDamageProductDetails(string productId, string size, string color, int quantity, decimal buyingPrice, string? reason, string? remarks,string adjustmentType)
        {
            ProductId = productId;
            Size = size;
            Color = color;
            Quantity = quantity;
            BuyingPrice = buyingPrice;
            Reason = reason;
            Remarks = remarks;
            CreatedAt= DateTime.UtcNow;
            AdjustmentType = adjustmentType;
        }
        public string Id { get; private set; }

        public string ProductId { get; private set; } = string.Empty;

        public string Size { get; private set; } = string.Empty;
        public string Color { get; private set; } = string.Empty;

        public int Quantity { get; private set; }

        // Price at the time the item became damaged
        public decimal BuyingPrice { get; private set; }

        public string? Reason { get; private set; } // Damaged, Torn, Wet, Lost, etc.

        public string? Remarks { get; private set; }
        public string? AdjustmentType { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    }
}
