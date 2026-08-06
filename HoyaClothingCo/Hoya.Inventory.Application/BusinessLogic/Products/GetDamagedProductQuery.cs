using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Entities.Misc;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Products
{
    public record GetDamagedProductQuery(): IRequest<List<DamagedProductDTO>>;
}
