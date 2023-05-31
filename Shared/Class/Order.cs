using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAmaris.Shared.Class
{
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public decimal Freight { get; set; }
        public string ShipCountry { get; set; }
    }
}
