using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Exhibitions
{
    public record CreateRefundCommand(
        string ExhibitionId,
        decimal Amount
    ) : IRequest<string>;
}