using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Exhibitions
{
    public record GetExhibitionOverviewQuery(string exhibitionId) : IRequest<ExhibitionOverviewDTO>;

    public class GetExhibitionOverviewQueryHandler: IRequestHandler<GetExhibitionOverviewQuery, ExhibitionOverviewDTO>
    {
        private readonly IExhibitionRepository _repo;

        public GetExhibitionOverviewQueryHandler(IExhibitionRepository repo)
        {
            _repo = repo;
        }

        public async Task<ExhibitionOverviewDTO> Handle(GetExhibitionOverviewQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetOverview(request.exhibitionId);
        }
    }
}
