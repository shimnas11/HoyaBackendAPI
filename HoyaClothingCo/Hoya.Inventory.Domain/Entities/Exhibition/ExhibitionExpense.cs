using System.Drawing;

namespace Hoya.Inventory.Domain.Entities.Exhibition
{
    public class ExhibitionExpense
    {
        public string ExhibitionId { get; set; }
        public Exhibition Exhibition { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public DateTime CreatedDate { get; set; }

        private ExhibitionExpense() { }

        public ExhibitionExpense(string exhibitionId, string name, decimal cost)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name must be added");
            if (string.IsNullOrEmpty(exhibitionId))
                throw new ArgumentException("ExhibitionId must be added");
            if (cost <= 0)
                throw new ArgumentException("Cost must be greater than zero");

            ExhibitionId = exhibitionId;
            Name = name;
            CreatedDate=DateTime.UtcNow;
            Cost = cost;
            Id = Guid.NewGuid().ToString();
        }


    }
}
