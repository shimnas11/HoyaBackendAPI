using Hoya.Inventory.Domain.Entities.Exhibition;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Exhibitions
{
    public class GetExhibitionQueryHandler : IRequestHandler<GetExhibitionQuery, List<Exhibition>>
    {
        private readonly IExhibitionRepository _repo;

        public GetExhibitionQueryHandler(IExhibitionRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Exhibition>> Handle(GetExhibitionQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetAll();
        }
    }

}
