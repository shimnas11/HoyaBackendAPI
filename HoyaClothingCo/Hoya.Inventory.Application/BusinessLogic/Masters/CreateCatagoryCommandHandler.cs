using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Application.BusinessLogic.Invoice;
using Hoya.Inventory.Domain.Entities;
using Hoya.Inventory.Domain.Entities.Masters;
using Hoya.Inventory.Domain.Interfaces;
using Hoya.Inventory.Domain.Interfaces.Masters;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Masters
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, string>
    {
        private readonly ICategoryRepository _repo;

        public CreateCategoryCommandHandler(ICategoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category(request.name);

            await _repo.AddAsync(category);
            return category.Id;
        }
    }
}
