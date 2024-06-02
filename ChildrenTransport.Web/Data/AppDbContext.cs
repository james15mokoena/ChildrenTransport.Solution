using ChildrenTransport.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ChildrenTransport.Web.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Taxi> Taxis { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<BankingDetails> BankingDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
