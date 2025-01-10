using RentCar.Model;
using Microsoft.EntityFrameworkCore;

namespace RentCar.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
    // you should add your path
    optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER,1433;TrustServerCertificate=True;");
   }
 
    // DbSet declarations
    public DbSet<CustomerModel> Customers { get; set; }
    public DbSet<SupplierModel> Suppliers { get; set; }
    public DbSet<LocationModel> Locations { get; set; }
    public DbSet<CarModel> Cars { get; set; }
    public DbSet<BookingModel> Bookings { get; set; }
    public DbSet<ReviewModel> Reviews { get; set; }
    public DbSet<RentImages> RentImages { get; set; } 
    public DbSet<UserModel> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Unique constraints
        modelBuilder.Entity<CustomerModel>()
            .HasIndex(c => c.IdentityNumber)
            .IsUnique();

        modelBuilder.Entity<CustomerModel>()
            .HasIndex(c => c.DrivingLicenseNumber)
            .IsUnique();

        modelBuilder.Entity<ReviewModel>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ReviewModel>()
            .HasOne(r => r.Supplier)
            .WithMany(s => s.Reviews)
            .HasForeignKey(r => r.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ReviewModel>()
            .HasOne(r => r.Car)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CarId)
            .OnDelete(DeleteBehavior.SetNull);
        

        modelBuilder.Entity<CustomerModel>()
            .Property(c => c.IdentityNumber)
            .HasMaxLength(11)
            .IsRequired();

        modelBuilder.Entity<CarModel>()
            .Property(c => c.DailyPrice)
            .HasPrecision(18, 2); // Explicit precision for decimal values
        
        modelBuilder.Entity<UserModel>()
            .HasMany(u => u.Customers) // Bir User'ın birden fazla Customer'ı olabilir
            .WithOne(c => c.User)      // Her Customer bir User'a bağlıdır
            .HasForeignKey(c => c.UserId) // Foreign key Customer içinde tanımlıdır
            .OnDelete(DeleteBehavior.Restrict); // User silindiğinde bağlı Customer'lar da silinsin

    }
}
