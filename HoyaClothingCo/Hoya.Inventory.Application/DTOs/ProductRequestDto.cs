using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoya.Inventory.Application.DTOs
{
    public class ProductRequestDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Color { get; set; }
        public decimal Cost { get; set; }
        public decimal SellingPrice { get; set; }
        public List<ProductSizeRequestDTO>? Sizes { get; set; }
    }


}
