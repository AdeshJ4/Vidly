using Microsoft.EntityFrameworkCore;
using Vidly.Models;

namespace Vidly.Data
{
    public class ApplicationDbContext : VidlyContext
    {
        public ApplicationDbContext ( DbContextOptions<ApplicationDbContext> options ) : base(options)
        {

        }


        /*
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public DbSet<MembershipType> MembershipType { get; set; }

        public DbSet<Genre> Genre { get; set; }

        */
    }
}