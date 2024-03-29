﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vidly.Areas.Identity.Data;
using Vidly.Models;

namespace Vidly.Data;

public class VidlyContext : IdentityDbContext<VidlyUser>
{
    public VidlyContext ()
    {
    }

    public VidlyContext(DbContextOptions<VidlyContext> options)
        : base(options)
    {
    }

    protected VidlyContext ( DbContextOptions options )
        : base(options)
    {
    }


    public DbSet<Customer> Customers { get; set; }
    public DbSet<Movie> Movies { get; set; }

    public DbSet<MembershipType> MembershipType { get; set; }

    public DbSet<Genre> Genre { get; set; }

    public DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
