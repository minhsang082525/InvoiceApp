using InvoiceApp.Models;
using Microsoft.EntityFrameworkCore;
namespace InvoiceApp.Services
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
            
        }

        public DbSet<Invoice> Invoices { get; set; } = null;
    }
}
