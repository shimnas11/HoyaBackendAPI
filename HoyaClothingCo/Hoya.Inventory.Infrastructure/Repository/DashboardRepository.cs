
using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Interfaces;

namespace Hoya.Inventory.Infrastructure.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IProductRepository _productRepo;
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IExhibitionRepository _exhibitionRepo;
        public DashboardRepository(IProductRepository productRepo, IInvoiceRepository invoiceRepo, IExhibitionRepository exhibitionRepo)
        {
            _productRepo = productRepo;
            _invoiceRepo = invoiceRepo;
            _exhibitionRepo = exhibitionRepo;
        }

        public async Task<DashboardDTO> GetDashboardOverviewAsync()
        {
            var products = await _productRepo.GetAllAsync();
            var invoices = await _invoiceRepo.GetAllAsync();
            var exhibitions = await _exhibitionRepo.GetAll();
            var response = new DashboardDTO()
            {
                TotalProducts = 0,
                TotalSold = 0,
                Profit = 0,
                Revenue = 0,
                TotalExpense = 0
            };
            products.ForEach(product =>
                   {
                       response.TotalProducts += product.Sizes.Sum(size => size.Quantity);

                   });

            invoices.ForEach(invoice =>
            {
                response.TotalSold += invoice.Items.Sum(item => item.Quantity);
                response.Revenue += invoice.TotalAmount;
                response.Profit += invoice.Items.Sum(item =>
                {
                    var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                    if (product != null)
                    {
                        return (item.Price - product.Cost) * item.Quantity;
                    }
                    return 0;
                });
                response.Profit -= invoice.DiscountAmount;
            });

            exhibitions.ForEach(exhibition =>
            {
                response.Profit -= exhibition.NetAmount;
                response.TotalExpense += exhibition.NetAmount;
            });
            return response;

        }
    }
}
