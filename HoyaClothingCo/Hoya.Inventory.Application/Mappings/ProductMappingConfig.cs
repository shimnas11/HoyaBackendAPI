
using Hoya.Inventory.Application.BusinessLogic.Invoice;
using Hoya.Inventory.Application.BusinessLogic.Products;
using Hoya.Inventory.Application.DTOs;
using Mapster;

namespace Hoya.Inventory.Application.Mappings
{
    public class ProductMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {

            config.NewConfig<ProductSizeRequestDTO, CreateProductSizeCommand>()
                .Map(dest => dest.size, src => src.Size)
                .Map(dest => dest.quantity, src => src.Quantity);

            config.NewConfig<ProductRequestDto, CreateProductCommand>()
                .Map(dest => dest.name, src => src.Name)
                .Map(dest => dest.code, src => src.Code)
                .Map(dest => dest.color, src => src.Color)
                .Map(dest => dest.cost, src => src.Cost)
                .Map(dest => dest.sellingPrice, src => src.SellingPrice)
                .Map(dest => dest.sizes, src => src.Sizes);

            config.NewConfig<ProductUpdateRequestDto, UpdateProductCommand>()
               .Map(dest => dest.Id, src => src.Id)
               .Map(dest => dest.name, src => src.Name)
               .Map(dest => dest.code, src => src.Code)
               .Map(dest => dest.color, src => src.Color)
               .Map(dest => dest.cost, src => src.Cost)
               .Map(dest => dest.sellingPrice, src => src.SellingPrice)
               .Map(dest => dest.sizes, src => src.Sizes);

            config.NewConfig<InvoiceProductDto, InvoiceProductCommand>()
                .Map(dest => dest.amount, src => src.Amount)
                .Map(dest => dest.productId, src => src.ProductId)
                .Map(dest => dest.size, src => src.Size)
                .Map(dest => dest.quantity, src => src.Quantity);

            config.NewConfig<InvoiceRequestDto, InvoiceCreateCommand>()

              .Map(dest => dest.discount, src => src.Discount)
              .Map(dest => dest.paymentMode, src => src.PaymentMode)
              .Map(dest => dest.exhibitionId, src => src.ExhibitionId)
              .Map(dest => dest.netAmount, src => src.NetAmount)
              .Map(dest => dest.products, src => src.Products);

        }
    }
}
