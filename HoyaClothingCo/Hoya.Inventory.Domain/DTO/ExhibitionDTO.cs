using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Domain.Entities.Exhibition;
using Hoya.Inventory.Domain.Entities;

namespace Hoya.Inventory.Domain.DTO
{
    public class ExhibitionDTO
    {
        public string Id { get; set; }
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

        public List<ExhibitionExpenseDTO> Expenses { get; set; }
        public List<InvoiceDto> Invoices { get; set; }
    }

    public class ExhibitionExpenseDTO
    {
        public string ExhibitionId { get; set; }
        public Exhibition Exhibition { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public DateTime CreatedDate { get; set; }

    }

    public class ExhibitionOverviewDTO
    {
        public string ExhibitionId { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Profit { get; set; }
    }

}
