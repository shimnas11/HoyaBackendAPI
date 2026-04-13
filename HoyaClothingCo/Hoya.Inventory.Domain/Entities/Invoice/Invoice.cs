using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Hoya.Inventory.Domain.Entities
{
    public class Invoice : AggregateRoot
    {

        public Invoice(string userId)
            : base(userId)
        {
            Id = Guid.NewGuid().ToString();
            ReferenceId = Common.CommonService.GenerateInvoiceRef("INV");
        }


        protected Invoice() : base() { }



        [BsonElement("products")]
        public List<InvoiceItem> Products { get; private set; } = new();

        public int TotalItems { get; private set; }

        public decimal TotalAmount { get; private set; }
        public decimal Discount { get; private set; }
        public decimal NetAmount { get; private set; }
        public string PaymentMode { get; private set; }
        public string? ExhibitionId { get; private set; }
        public string ReferenceId { get; private set; }
        public string Status { get; private set; }


        public void AddInvoiceItems(List<InvoiceItem> items)
        {
            foreach (var item in items)
            {
                Products.Add(new InvoiceItem(item.ProductId, item.Size, item.Quantity, item.Amount));
            }

            TotalItems = Products.Sum(s => s.Quantity);
            TotalAmount = Products.Sum(s => s.Amount);
            Status = "Created";
        }

        public void AddDiscount(decimal discount)
        {
            Discount = discount;
        }

        public void SetNetAmount(decimal netAmount)
        {
            NetAmount = netAmount;
        }

        public void SetPaymentMode(string paymentMode)
        {
            PaymentMode = paymentMode;
        }

        public void SetExhibitionId(string exhibitionId)
        {
            ExhibitionId = exhibitionId;
        }
        


    }
}
