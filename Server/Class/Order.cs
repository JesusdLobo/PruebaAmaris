using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAmaris.Shared.Class
{
    public class Order
    { 
        public int OrderID { get; set; }
        public string? CustomerID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Freight { get; set; }
        public string? ShipCountry { get; set; }
    }

    public class OrderDTO
    { 
        public string? CustomerID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Freight { get; set; }
        public string? ShipCountry { get; set; }
    }
}
