using System.ComponentModel.DataAnnotations;

namespace Hoya.Inventory.Application.DTOs
{
    public class CreateExpenseDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ExhibitionId { get; set; }

        [Required]
        public decimal Cost { get; set; }
    }
}
