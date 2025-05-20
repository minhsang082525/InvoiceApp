using InvoiceApp.Models;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InvoiceApp.Pages.Invoices
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;
        public List<Invoice> invoiceList = new();

        [BindProperty(SupportsGet = true)]
        public string StatusFilter { get; set; } = string.Empty;


        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string SortDirection { get; set; } = "desc";


        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }


        public void OnGet()
        {
            var query = context.Invoices.AsQueryable();
            var invoices = query.ToList();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                query = query.Where(i =>
                    i.Number.Contains(SearchTerm) ||
                    i.ClientName.Contains(SearchTerm));
            }

            if (!string.IsNullOrWhiteSpace(StatusFilter))
            {
                query = query.Where(i => i.Status == StatusFilter);
            }

            if (!string.IsNullOrEmpty(SortBy))
            {
                bool ascending = SortDirection == "asc";

                switch (SortBy)
                {
                    case "Id":
                        query = ascending ? query.OrderBy(i => i.Id) : query.OrderByDescending(i => i.Id);
                        break;
                    case "Number":
                        query = ascending ? query.OrderBy(i => i.Number) : query.OrderByDescending(i => i.Number);
                        break;
                    case "ClientName":
                        query = ascending ? query.OrderBy(i => i.ClientName) : query.OrderByDescending(i => i.ClientName);
                        break;
                    case "Total":
                        invoices = SortDirection == "asc"
                            ? invoices.OrderBy(i => i.UnitPrice * i.Quantity).ToList()
                            : invoices.OrderByDescending(i => i.UnitPrice * i.Quantity).ToList();
                        break;

                    case "IssueDate":
                        query = ascending ? query.OrderBy(i => i.IssueDate) : query.OrderByDescending(i => i.IssueDate);
                        break;
                    case "DueDate":
                        query = ascending ? query.OrderBy(i => i.DueDate) : query.OrderByDescending(i => i.DueDate);
                        break;
                    default:
                        query = query.OrderByDescending(i => i.Id);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(i => i.Id);
            }

            invoiceList = query.ToList();
        }
    }
}
