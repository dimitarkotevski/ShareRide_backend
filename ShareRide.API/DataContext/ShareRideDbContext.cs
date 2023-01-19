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
    public DbSet<ProfilePicture> ProfilePicture { get; set; }
    public DbSet<VerificationCode> VerificationCodes { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=ShareRideDb;Integrated Security=True;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //User -> Role
        modelBuilder
                .Entity<User>()
                .HasOne(c => c.Role)
                .WithMany(c => c.Users)
                .HasForeignKey(c => c.RoleId);

        //User -> ProfilePicture
        modelBuilder
            .Entity<User>()
            .HasOne<ProfilePicture>(c => c.ProfilePicture)
            .WithOne(c => c.User)
            .HasForeignKey<User>(c => c.ProfilePictureId);
        //User -> VerificationCode
        modelBuilder
            .Entity<User>()
            .HasOne<VerificationCode>(c => c.VerificationCode)
            .WithOne(c => c.User)
            .HasForeignKey<User>(c => c.VerificationCodeId);


        base.OnModelCreating(modelBuilder);
    }

}
