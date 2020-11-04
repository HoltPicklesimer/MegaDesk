using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegaDesk.Data;
using MegaDesk.Models;

namespace MegaDesk.Pages.DeskQuotes
{
    public class EditModel : PageModel
    {
        private readonly MegaDesk.Data.MegaDeskContext _context;

        public EditModel(MegaDesk.Data.MegaDeskContext context)
        {
            _context = context;
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DeskQuote = await _context.DeskQuote
                .Include(d => d.DeliveryOption)
                .Include(d => d.Desk).FirstOrDefaultAsync(m => m.DeskQuoteId == id);

            if (DeskQuote == null)
            {
                return NotFound();
            }

            ViewData["DeliveryOptionId"] = new SelectList(_context.Set<DeliveryOption>(), "DeliveryOptionId", "DeliveryName");
            ViewData["DesktopMaterialId"] = new SelectList(_context.Set<DesktopMaterial>(), "DesktopMaterialId", "MaterialName");

            return Page();
        }

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

            DeskQuote.QuotePrice = DeskQuote.CalculatePriceQuote();

            _context.Attach(DeskQuote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeskQuoteExists(DeskQuote.DeskQuoteId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DeskQuoteExists(int id)
        {
            return _context.DeskQuote.Any(e => e.DeskQuoteId == id);
        }
    }
}
