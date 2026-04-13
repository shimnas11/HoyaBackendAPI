using Hoya.Inventory.Domain.Entities.Exhibition;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Exhibitions
{
    public record GetExhibitionQuery() : IRequest<List<Exhibition>>;
}
