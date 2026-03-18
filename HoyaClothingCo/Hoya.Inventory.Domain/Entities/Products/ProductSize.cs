namespace Hoya.Inventory.Domain.Entities
{
    public class ProductSize
    {
        public string Size { get; private set; }
        public int Quantity { get; private set; }

        // MongoDB needs this
        private ProductSize() { }

        public ProductSize(string size, int quantity)
        {
            Size = size;
            Quantity = quantity;
        }

        public void AddStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Invalid quantity");

            Quantity += quantity;
        }
        public void UpdateStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Invalid quantity");

            Quantity = quantity;
        }

        public void RemoveStock(int quantity)
        {
            if (quantity > Quantity)
                throw new InvalidOperationException("Insufficient stock");

            Quantity -= quantity;
        }
    }
}