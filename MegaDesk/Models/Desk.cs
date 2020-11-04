using MegaDesk.Models;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MegaDesk.Models
{
    public class Desk
    {
        // Constants
        public const decimal MIN_WIDTH = 24;
        public const decimal MAX_WIDTH = 96;
        public const decimal MIN_DEPTH = 12;
        public const decimal MAX_DEPTH = 48;
        public const int MIN_DRAWERS = 0;
        public const int MAX_DRAWERS = 7;

        // Public variables
        public int DeskId { get; set; }

        [Range((double)MIN_WIDTH, (double)MAX_WIDTH)]
        [Required]
        public decimal Width { get; set; }

        [Range((double)MIN_DEPTH, (double)MAX_DEPTH)]
        [Required]
        public decimal Depth { get; set; }

        [Range((double)MIN_DRAWERS, (double)MAX_DRAWERS)]
        [Required]
        public int Drawers { get; set; }

        [Display(Name = "Material")]
        [Required]
        public int DesktopMaterialId { get; set; }

        // Navigation Properties
        public DesktopMaterial DesktopMaterial { get; set; }
    }
}
