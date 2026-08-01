using System.Collections.Generic;
using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Dashboard
{
    public class GetHotSellingProductQueryHandler : IRequestHandler<GetHotSellingProductQuery, List<HotSellingProductDTO>>
    {
        private readonly IDashboardRepository _repo;

        public GetHotSellingProductQueryHandler(IDashboardRepository repo)
        {
            _repo = repo;
        }

       

        public Task<List<HotSellingProductDTO>> Handle(GetHotSellingProductQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetHotSellingProductsAsync();
        }
    }
    public class GetExhibitionOverviewQueryHandler : IRequestHandler<GetExhibitionOverviewQuery, List<ExhibitionOverviewDTO>>
    {
        private readonly IDashboardRepository _repo;

        public GetExhibitionOverviewQueryHandler(IDashboardRepository repo)
        {
            _repo = repo;
        }



        public Task<List<ExhibitionOverviewDTO>> Handle(GetExhibitionOverviewQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetExhibitionOverviewAsync();
        }
    }
}
