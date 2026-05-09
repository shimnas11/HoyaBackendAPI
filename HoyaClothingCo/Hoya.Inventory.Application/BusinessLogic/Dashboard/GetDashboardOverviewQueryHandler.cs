using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Dashboard
{
    public class GetDashboardOverviewQueryHandler : IRequestHandler<GetDashboardOverviewQuery, DashboardDTO>
    {
        private readonly IDashboardRepository _repo;

        public GetDashboardOverviewQueryHandler(IDashboardRepository repo)
        {
            _repo = repo;
        }

        public Task<DashboardDTO> Handle(GetDashboardOverviewQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetDashboardOverviewAsync();
        }
    }
}
