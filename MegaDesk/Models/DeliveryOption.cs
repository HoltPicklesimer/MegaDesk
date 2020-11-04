using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MegaDesk.Models
{
    public class DeliveryOption
    {
        public int DeliveryOptionId { get; set; }
        public string DeliveryName { get; set; }
        public decimal SmallPrice { get; set; }
        public decimal MediumPrice { get; set; }
        public decimal LargePrice { get; set; }
    }
}
