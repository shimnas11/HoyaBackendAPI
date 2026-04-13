using Hoya.Inventory.Domain.DTO;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Exhibitions
{
    public record GetExhibitionDetailsQuery(string id) : IRequest<ExhibitionDTO>;
}
