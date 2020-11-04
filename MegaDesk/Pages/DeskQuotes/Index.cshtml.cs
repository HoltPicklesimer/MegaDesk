using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDesk.Data;
using MegaDesk.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MegaDesk.Pages.DeskQuotes
{
    public class IndexModel : PageModel
    {
        private readonly MegaDesk.Data.MegaDeskContext _context;

        public IndexModel(MegaDesk.Data.MegaDeskContext context)
        {
            _context = context;
        }

        public IList<DeskQuote> DeskQuote { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string OrderBy { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;

        public SelectList Customer { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CustomerName { get; set; }


        public IList<DesktopMaterial> DesktopMaterial { get; set; }
        public IList<Desk> Desk { get; set; }
        public IList<DeliveryOption> DeliveryOption { get; set; }


        public async Task OnGetAsync()
        {
            IQueryable<string> customerQuery = from d in _context.DeskQuote
                                               orderby d.CustomerName
                                               select d.CustomerName;

            var deskQuotes = from d in _context.DeskQuote select d;

            if (!string.IsNullOrEmpty(SearchString))
            {
                deskQuotes = deskQuotes.Where(d => d.CustomerName.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(OrderBy))
            {
                switch (OrderBy)
                {
                    case "customerName":
                        deskQuotes = deskQuotes.OrderBy(d => d.CustomerName);
                        break;
                    case "quoteDate":
                        deskQuotes = deskQuotes.OrderBy(d => d.QuoteDate);
                        break;
                    default:
                        deskQuotes = deskQuotes.OrderBy(d => d.CustomerName);
                        break;
                }
            }
            else
            {
                deskQuotes = deskQuotes.OrderBy(d => d.CustomerName);
            }

            Customer = new SelectList(await customerQuery.Distinct().ToListAsync());
            DeskQuote = await deskQuotes
                .Include(d => d.DeliveryOption)
                .Include(d => d.Desk)
                .Include(d => d.Desk.DesktopMaterial)
                .ToListAsync();


            //DeskQuote = await _context.DeskQuote
            //    .Include(d => d.Delivery)
            //    .Include(d => d.Desk).ToListAsync();
        }
    }
}
