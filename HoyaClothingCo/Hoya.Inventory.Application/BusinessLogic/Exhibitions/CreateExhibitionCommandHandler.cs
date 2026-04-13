using Hoya.Inventory.Domain.Entities.Exhibition;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Exhibitions
{
    public class CreateExhibitionCommandHandler : IRequestHandler<CreateExhibitionCommand, string>
    {
        private readonly IExhibitionRepository _repo;

        public CreateExhibitionCommandHandler(IExhibitionRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(CreateExhibitionCommand request, CancellationToken cancellationToken)
        {
          
            var exhibition = new Exhibition(request.userId );
            exhibition.SetExhibitionDetails(request.name, request.place, request.runBy, request.startDate, request.endDate, request.bookingCost);
            await _repo.AddAsync(exhibition);
            return "Exhibtion created successfully";
        }
    }
}
