using Hoya.Inventory.Domain.Entities;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Products
{
    public class GetProductHandler : IRequestHandler<GetProductQuery, List<Product>>
    {
        private readonly IProductRepository _repo;

        public GetProductHandler(IProductRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetAllAsync();
        }
    }
}
