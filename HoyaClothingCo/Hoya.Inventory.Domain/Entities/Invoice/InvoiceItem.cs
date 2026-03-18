namespace Hoya.Inventory.Domain.Entities
{
    public class InvoiceItem 
    {

        private InvoiceItem() { }

        public InvoiceItem(string productId, string size, int quantity, decimal amount)
        {
            if (string.IsNullOrEmpty(productId))
                throw new ArgumentException("ProductId must be added");
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");
            ProductId = productId;
            Size = size;
            Quantity = quantity;
            Amount = amount;
        }

        public string ProductId { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public Decimal Amount { get; set; }
    }
}
