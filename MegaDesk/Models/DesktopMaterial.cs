using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MegaDesk.Models
{
    public class DesktopMaterial
    {
        public int DesktopMaterialId { get; set; }
        public string MaterialName { get; set; }
        public decimal Cost { get; set; }
        public string ImageUrl { get; set; }
    }
}
