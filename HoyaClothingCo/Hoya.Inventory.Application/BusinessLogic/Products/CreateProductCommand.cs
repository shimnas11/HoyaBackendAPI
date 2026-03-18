using System.Net.Http.Headers;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Products
{

    public record CreateProductCommand(string name,string code, string color, decimal cost,decimal sellingPrice,string userId,

       List<CreateProductSizeCommand> sizes ) : IRequest<string>;

}
