namespace Hoya.Inventory.Application.DTOs
{
    public class ProductUpdateRequestDto: ProductRequestDto
    {
        public string Id { get; set; }
    }
    public class CategoryCreateDTO 
    {
        public string Name { get; set; }
    }

    public class MaterialTypeCreateDTO
    {
        public string Name { get; set; }
    }

    public class CreateDamageDto
    {
        public string ProductId { get; set; }

        public string Size { get; set; } 

        public int Quantity { get; set; }

        public string? Reason { get; set; }

        public string? Remarks { get; set; }

        public string AdjustmentType { get; set; } 
    }

}
