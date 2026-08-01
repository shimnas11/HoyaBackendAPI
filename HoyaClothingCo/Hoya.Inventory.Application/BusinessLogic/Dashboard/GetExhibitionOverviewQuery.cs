using Hoya.Inventory.Domain.DTO;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Dashboard
{
    public record GetExhibitionOverviewQuery : IRequest<List<ExhibitionOverviewDTO>>;
}
