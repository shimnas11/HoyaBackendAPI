using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoya.Inventory.Application.DTOs
{
    public class CreateExhibitionDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public string RunBy { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public Decimal BookingCost { get; set; }
    }


}
