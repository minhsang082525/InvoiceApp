using InvoiceApp.Models;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceApp.Pages.Invoices
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public DeleteModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public Invoice? Invoice { get; set; }

        public IActionResult OnGet(int id)
        {
            Invoice = context.Invoices.Find(id);
            if (Invoice == null)
            {
                return RedirectToPage("/Invoices/Index");
            }

            return Page();
        }


        public IActionResult OnPost()
        {
            if (Invoice == null)
            {
                return RedirectToPage("/Invoices/Index");
            }

            var invoiceToDelete = context.Invoices.Find(Invoice.Id);
            if (invoiceToDelete != null)
            {
                context.Invoices.Remove(invoiceToDelete);
                context.SaveChanges();
            }

            return RedirectToPage("/Invoices/Index");
        }
    }
}
