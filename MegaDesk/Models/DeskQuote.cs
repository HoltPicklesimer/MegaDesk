using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MegaDesk.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegaDesk.Models
{
    public class DeskQuote
    {
        // Constants
        private const decimal BASE_COST = 200M;
        private const decimal DRAWER_COST = 50M;
        private const decimal SURFACEAREA_COST = 1M;

        // Public variables
        public int DeskQuoteId { get; set; }

        [Display(Name = "Customer Name")]
        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string CustomerName { get; set; }
        public int DeskId { get; set; }

        [Display(Name = "Delivery Option")]
        [Required]
        public int DeliveryOptionId { get; set; }

        [Display(Name = "Quote Price")]
        [Column(TypeName = "decimal(18, 2)")]
        [DataType(DataType.Currency)]
        public decimal QuotePrice { get; set; }

        [Display(Name = "Quote Date")]
        public DateTime QuoteDate { get; set; }

        // Navigation Properties
        public Desk Desk { get; set; }
        public DeliveryOption DeliveryOption { get; set; }

        public decimal CalculatePriceQuote()
        {
            // Surface Area Cost
            var surfaceArea = Desk.Width * Desk.Depth;
            var surfaceAreaCost = 0M;
            if (surfaceArea > 1000)
                surfaceAreaCost = (surfaceArea - 1000) * SURFACEAREA_COST;

            // Drawer Cost
            var drawerCost = Desk.Drawers * DRAWER_COST;

            // Material Cost
            decimal materialCost = Desk.DesktopMaterial.Cost;

            // Delivery Cost
            var deliveryCost = GetDeliveryCost(surfaceArea);

            // Calculate the desk price
            QuotePrice = BASE_COST + surfaceAreaCost + drawerCost + materialCost + deliveryCost;
            return QuotePrice;
        }

        private decimal GetDeliveryCost(decimal surfaceArea)
        {
            decimal deliveryCost;

            if (surfaceArea < 1000)
                deliveryCost = DeliveryOption.SmallPrice;
            else if (surfaceArea <= 2000)
                deliveryCost = DeliveryOption.MediumPrice;
            else
                deliveryCost = DeliveryOption.LargePrice;

            return deliveryCost;
        }
    }
}
