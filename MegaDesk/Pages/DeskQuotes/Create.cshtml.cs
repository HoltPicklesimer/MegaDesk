using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MegaDesk.Data;
using MegaDesk.Models;

namespace MegaDesk.Pages.DeskQuotes
{
    public class CreateModel : PageModel
    {
        private readonly MegaDesk.Data.MegaDeskContext _context;

        public CreateModel(MegaDesk.Data.MegaDeskContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DeliveryOptionId"] = new SelectList(_context.Set<DeliveryOption>(), "DeliveryOptionId", "DeliveryName");
            ViewData["DesktopMaterialId"] = new SelectList(_context.Set<DesktopMaterial>(), "DesktopMaterialId", "MaterialName");

            return Page();
        }

        [BindProperty]
        public DeskQuote DeskQuote { get; set; }

        [BindProperty]
        public DeliveryOption DeliveryOption { get; set; }

        [BindProperty]
        public DesktopMaterial DesktopMaterial { get; set; }

        [BindProperty]
        public int Width { get; set; }

        [BindProperty]
        public int Depth { get; set; }

        [BindProperty]
        public int Drawers { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            DeskQuote.QuoteDate = DateTime.Now;

            // Get the material for calculating the price
            DeskQuote.Desk.DesktopMaterial = _context.DesktopMaterial
                .FirstOrDefault(d => d.DesktopMaterialId == DeskQuote.Desk.DesktopMaterialId);
            // Get the delivery option for calculating the price
            DeskQuote.DeliveryOption = _context.DeliveryOption
                .FirstOrDefault(d => d.DeliveryOptionId == DeskQuote.DeliveryOptionId);

            _context.Desk.Add(DeskQuote.Desk);
            await _context.SaveChangesAsync();

            DeskQuote.DeskId = DeskQuote.Desk.DeskId;
            DeskQuote.QuotePrice = DeskQuote.CalculatePriceQuote();

            _context.DeskQuote.Add(DeskQuote);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
