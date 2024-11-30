using Microsoft.EntityFrameworkCore;
using SmartEnums.Models;

namespace SmartEnums.Persistence;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=test;User Id=postgres;Password=admin;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");

        modelBuilder.Entity<User>().HasKey(x => x.Id);

        modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<User>().Property(x => x.Name).IsRequired().HasMaxLength(50);

        modelBuilder.Entity<User>()
            .HasOne<Role>()
            .WithMany()
            .HasForeignKey("RoleId")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        // Roles
        modelBuilder.Entity<Role>().ToTable("Roles");

        modelBuilder.Entity<Role>().HasKey(x => x.Id);

        modelBuilder.Entity<Role>().Property(x => x.Id).HasPrecision(14, 2).IsRequired();

        modelBuilder.Entity<Role>().Property(x => x.Name).IsRequired().HasMaxLength(50);

        modelBuilder.Entity<Role>().Property(x => x.Description).HasMaxLength(250);

        modelBuilder.Entity<Role>(x =>
            x.HasData(Role.Admin, Role.Contributor, Role.Guest)
        );
    }
}