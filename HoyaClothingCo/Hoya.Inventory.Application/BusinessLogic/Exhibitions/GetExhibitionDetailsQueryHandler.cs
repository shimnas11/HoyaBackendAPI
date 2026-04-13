using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Exhibitions
{
    public class GetExhibitionDetailsQueryHandler : IRequestHandler<GetExhibitionDetailsQuery, ExhibitionDTO>
    {
        private readonly IExhibitionRepository _repo;

        public GetExhibitionDetailsQueryHandler(IExhibitionRepository repo)
        {
            _repo = repo;
        }

        public async Task<ExhibitionDTO> Handle(GetExhibitionDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetDetails(request.id);
        }
    }

}
