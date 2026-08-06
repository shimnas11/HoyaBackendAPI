using Hoya.Inventory.Domain.Entities;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Products
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductRepository _repo;

        public GetProductByIdHandler(IProductRepository repo)
        {
            _repo = repo;
        }

        public Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetProductByIdAsync(request.id);
        }
    }
}
