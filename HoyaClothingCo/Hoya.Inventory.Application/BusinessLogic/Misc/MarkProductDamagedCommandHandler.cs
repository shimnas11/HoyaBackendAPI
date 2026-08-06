using Hoya.Inventory.Domain.Entities.Misc;
using Hoya.Inventory.Domain.Interfaces;
using Hoya.Inventory.Domain.Interfaces.Masters;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Misc
{
    public class MarkProductDamagedCommandHandler : IRequestHandler<MarkProductDamagedCommand, string>
    {
        private readonly IProductRepository _repo;
        private readonly IDamagedProductRepository _damagedProductRepository;

        public MarkProductDamagedCommandHandler(IProductRepository repo, IDamagedProductRepository damagedProductRepository)
        {
            _repo = repo;
            _damagedProductRepository = damagedProductRepository;
        }
        public async Task<string> Handle(
    MarkProductDamagedCommand request,
    CancellationToken cancellationToken)
        {
            var product = await _repo.GetProductByIdAsync(request.ProductId);

            if (product == null)
                throw new Exception("Product not found.");

            var size = product.Sizes.FirstOrDefault(x => x.Size == request.Size);

            if (size == null)
                throw new Exception("Size not found.");

            if (size.Quantity < request.Quantity)
                throw new Exception("Insufficient stock.");

            // Reduce stock
            product.DeductStock(request.Size, request.Quantity);

            await _repo.UpdateAsync(product, request.ProductId);

            // Create damage record
            var damagedProduct = new DamagedProduct();
            damagedProduct.SetDamageProductDetails(
                 request.ProductId,
                request.Size,
                 product.Color,
                request.Quantity,
                product.Cost,
                 request.Reason,
             request.Remarks,
             request.adjustmentType
            );

            await _damagedProductRepository.AddAsync(damagedProduct);

            return "Product marked as damaged successfully.";
        }
    }
}
