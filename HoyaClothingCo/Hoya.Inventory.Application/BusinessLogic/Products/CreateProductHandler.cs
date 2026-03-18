using Hoya.Inventory.Domain.Entities;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Products
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, string>
    {
        private readonly IProductRepository _repo;

        public CreateProductHandler(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var sizes = new List<ProductSize>();
            
            foreach (var item in request.sizes)
            {
                sizes.Add(new ProductSize(item.size, item.quantity));
            }
            var product = new Domain.Entities.Product(request.name,request.code, request.color,request.cost,request.sellingPrice,request.userId);
            product.AddSizes(sizes);


            await _repo.AddAsync(product);
            return "Product added successfully";
        }
    }
}
