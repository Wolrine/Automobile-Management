using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AutomobileLibrary.DataAccess;

public partial class MyStockContext : DbContext
{
    public MyStockContext()
    {
    }

    public MyStockContext(DbContextOptions<MyStockContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId);

            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.CarName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.HasData(
                new Car() { CarId = 1, CarName = "Accord", Manufacturer = "Honda Motor Company", Price = 249700000, ReleaseYear = 2021 },
                new Car() { CarId = 2, CarName = "Clarity", Manufacturer = "Honda Motor Company", Price = 33400, ReleaseYear = 2021 },
                new Car() { CarId = 3, CarName = "BMW 8 Series", Manufacturer = "BMW", Price = 85000, ReleaseYear = 2021 },
                new Car() { CarId = 4, CarName = "Audi A6", Manufacturer = "Audi", Price = 14000, ReleaseYear = 2020 }
            );
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
