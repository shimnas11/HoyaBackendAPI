using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Products
{
    public record CreateProductSizeCommand(string size, int quantity) : IRequest; 

}
