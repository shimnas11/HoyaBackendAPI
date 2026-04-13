using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Exhibitions
{

    public record CreateExhibitionExpenseCommand(
        string ExhibitionId,
        string Name,
        decimal Cost
    ) : IRequest<string>;
}