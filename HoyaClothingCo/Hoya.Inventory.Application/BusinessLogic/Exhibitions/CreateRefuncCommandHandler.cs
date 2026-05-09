using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Exhibitions
{
    public class CreateRefundCommandHandler
  : IRequestHandler<CreateRefundCommand, string>
    {
        private readonly IExhibitionRepository _repo;

        public CreateRefundCommandHandler(IExhibitionRepository repo)
        {
            _repo = repo;
        }
        public async Task<string> Handle(
            CreateRefundCommand request,
            CancellationToken cancellationToken)
        {
            var exhibition = await _repo.GetByIdAsync(request.ExhibitionId);
            exhibition.Refund(request.Amount);
            await _repo.UpdateAsync(exhibition);
            return "Refunc processed successfully";
        }
    }

}
