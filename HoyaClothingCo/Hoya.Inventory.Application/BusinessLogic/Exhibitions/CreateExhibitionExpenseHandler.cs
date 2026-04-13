using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Domain.Entities.Exhibition;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Exhibitions
{
    public class CreateExhibitionExpenseHandler
    : IRequestHandler<CreateExhibitionExpenseCommand, string>
    {
        private readonly IExhibitionRepository _repo;

        public CreateExhibitionExpenseHandler(IExhibitionRepository repo)
        {
            _repo = repo;
        }
        public async Task<string> Handle(
            CreateExhibitionExpenseCommand request,
            CancellationToken cancellationToken)
        {
            var exhibition = await _repo.GetByIdAsync(request.ExhibitionId);
            exhibition.AddExpenses(new ExhibitionExpense(request.ExhibitionId, request.Name, request.Cost));
            exhibition.SetNetAmount();
            await _repo.UpdateAsync(exhibition);
            return "Expense added successfully";
        }
    }

}
