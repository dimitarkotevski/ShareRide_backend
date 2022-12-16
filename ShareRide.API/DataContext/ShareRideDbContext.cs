using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShareRide.API.Models;

namespace ShareRide.API.DataContext;

public partial class ShareRideDbContext : DbContext
{
    public ShareRideDbContext()
    {
    }

    public ShareRideDbContext(DbContextOptions<ShareRideDbContext> options)
        : base(options)
    {
    }
    public  DbSet<User> Users { get; set; }
    public  DbSet<Role> Roles { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=ShareRideDb;Integrated Security=True;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
