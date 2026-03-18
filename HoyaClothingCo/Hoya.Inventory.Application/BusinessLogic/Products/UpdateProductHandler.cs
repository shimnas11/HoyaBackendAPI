using System.Drawing;
using System.Xml.Linq;
using Hoya.Inventory.Domain.Entities;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Products
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, string>
    {
        private readonly IProductRepository _repo;

        public UpdateProductHandler(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repo.GetProductByIdAsync(request.Id);

            if (product == null)
                return null;

            // 2️⃣ Prepare sizes
            var sizes = new List<ProductSize>();

            foreach (var item in request.sizes)
            {
                sizes.Add(new Domain.Entities.ProductSize(item.size, item.quantity));
            }

            // 3️⃣ Update product fields
            product.UpdateDetails(request.name, request.code,request.color, request.cost, request.sellingPrice);

            // 4️⃣ Update sizes / quantity
            product.UpdateSizes(sizes);

            // 5️⃣ Save changes
            await _repo.UpdateAsync(product, request.Id);

            return product.Id;
        }

       


    }
}
