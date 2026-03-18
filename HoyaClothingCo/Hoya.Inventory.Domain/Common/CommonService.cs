using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoya.Inventory.Domain.Common
{
    public static class CommonService
    {
        public static string GenerateInvoiceRef(string type)
        {
            var datePart = DateTime.UtcNow.ToString("yyyyMMdd");

            var randomPart = Guid.NewGuid().ToString("N").Substring(0, 4).ToUpper();

            return $"{type}-{datePart}-{randomPart}";
        }
    }
}
