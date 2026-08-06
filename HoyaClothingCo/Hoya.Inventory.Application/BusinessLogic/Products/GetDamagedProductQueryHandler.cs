using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Entities.Misc;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Products
{
    public class GetDamagedProductQueryHandler : IRequestHandler<GetDamagedProductQuery, List<DamagedProductDTO>>
    {
        private readonly IDamagedProductRepository _repo;

        public GetDamagedProductQueryHandler(IDamagedProductRepository repo)
        {
            _repo = repo;
        }

        public Task<List<DamagedProductDTO>> Handle(GetDamagedProductQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetAllAsync();
        }
    }
}
