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

}
