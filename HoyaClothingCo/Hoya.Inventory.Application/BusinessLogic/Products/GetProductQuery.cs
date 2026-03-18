using Hoya.Inventory.Domain.Entities;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Products
{
    public record GetProductQuery() : IRequest<List<Product>>;
    
}
