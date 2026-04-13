using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoya.Inventory.Domain.Entities.Exhibition
{
    public class Exhibition : AggregateRoot
    {
        public Exhibition(string userId)
          : base(userId)
        {
            Id = Guid.NewGuid().ToString();
        }

        protected Exhibition() : base() { }

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
        //public List<Invoice> Invoices { get; set; }

        public void SetExhibitionDetails(string name, string place, string runBy, DateTime startDate, DateTime endDate, decimal bookingCost)
        {
            Name = name;
            Place = place;
            RunBy = runBy;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfDays = (EndDate - StartDate).Days + 1;
            BookingCost = bookingCost;
        }



        public void AddExpenses(ExhibitionExpense item)
        {
            if (Expenses == null)
            {
                Expenses = new List<ExhibitionExpense>();
            }


            Expenses.Add(new ExhibitionExpense(item.ExhibitionId, item.Name, item.Cost));
        }

        public void SetNetAmount()
        {
            TotalExpense = Expenses.Sum(s => s.Cost);
            NetAmount = TotalExpense + BookingCost;
        }

        public void SetProfit(decimal profit)
        {
            Profit = profit;
        }

        public void SetLoss(decimal loss)
        {
            Loss = loss;
        }

    }
}
