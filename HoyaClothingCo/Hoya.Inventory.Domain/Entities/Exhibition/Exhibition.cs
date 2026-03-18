using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoya.Inventory.Domain.Entities.Exhibition
{
    public class Exhibition : AggregateRoot
    {
        public string Name { get; set; }
        public string Place { get; set; }
        public string RunBy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }
        public decimal BookingCost { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Profit { get; set; }
        public decimal Loss { get; set; }

        public List<ExhibitionExpense> Expenses { get; set; }
        public List<Invoice> Invoices { get; set; }
    }

    public class ExhibitionExpense
    {
        public string ExhibitionId { get; set; }
        public Exhibition Exhibition { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
