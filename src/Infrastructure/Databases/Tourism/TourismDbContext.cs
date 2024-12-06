namespace CleanMinimalApi.Infrastructure.Databases.Tourism;

using System.Reflection;
using CleanMinimalApi.Infrastructure.Databases.Tourism.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class TourismDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Sponsor> Sponsors { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<BrandCategory> BrandCategories { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Service> Services { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
            //Console.WriteLine(Directory.GetCurrentDirectory());
            //optionsBuilder.UseSqlServer("Server=MSI\\TANPN;Uid=sa;Pwd=12345;Database=TourismDB;TrustServerCertificate=True;Integrated Security=true; Trusted_Connection=true");
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("TourismDB"));
        }
    }
    public TourismDbContext(DbContextOptions<TourismDbContext> options) : base(options)
    {

    }
    public TourismDbContext()
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        #region Sponsor

        modelBuilder.Entity<Sponsor>()
            .ToTable("Sponsor")
            .HasKey(a => a.Id);

        #endregion Sponsor

        #region Brand

        modelBuilder.Entity<Brand>()
           .ToTable("Brand")
           .HasKey(b => b.Id);

        modelBuilder.Entity<Brand>()
           .ToTable("Brand")
           .HasOne(b => b.District)
           .WithMany(d => d.Brands)
           .HasForeignKey(b => b.DistrictId);

        #endregion Brand

        #region BrandCategory

        modelBuilder.Entity<BrandCategory>()
           .ToTable("BrandCategory")
           .HasKey(a => a.Id);

        modelBuilder.Entity<BrandCategory>()
          .ToTable("BrandCategory")
          .HasOne(b => b.Brand)
          .WithMany(d => d.BrandCategories)
          .HasForeignKey(b => b.BrandId);

        modelBuilder.Entity<BrandCategory>()
          .ToTable("BrandCategory")
          .HasOne(b => b.Category)
          .WithMany(d => d.BrandCategories)
          .HasForeignKey(b => b.CategoryId);

        #endregion BrandCategory

        #region Category

        modelBuilder.Entity<Category>()
           .ToTable("Category")
           .HasKey(a => a.Id);

        #endregion Category

        #region City

        modelBuilder.Entity<City>()
           .ToTable("City")
           .HasKey(a => a.Id);

        modelBuilder.Entity<City>()
        .ToTable("City")
        .HasOne(b => b.Province)
        .WithMany(d => d.Cities)
        .HasForeignKey(b => b.ProvinceId);

        #endregion City

        #region District

        modelBuilder.Entity<District>()
           .ToTable("District")
           .HasKey(a => a.Id);

        modelBuilder.Entity<District>()
        .ToTable("District")
        .HasOne(b => b.City)
        .WithMany(d => d.Districts)
        .HasForeignKey(b => b.CityId);

        #endregion District

        #region Province

        modelBuilder.Entity<Province>()
           .ToTable("Province")

           .HasKey(a => a.Id);
        #endregion Province

        #region Order

        modelBuilder.Entity<Order>()
           .ToTable("Order")
           .HasKey(a => a.Id);

        modelBuilder.Entity<Order>()
        .ToTable("Order")
        .HasOne(b => b.User)
        .WithMany(d => d.Orders)
        .HasForeignKey(b => b.UserId);

        #endregion Order

        #region OrderDetail

        modelBuilder.Entity<OrderDetail>()
           .ToTable("OrderDetail")
           .HasKey(a => a.Id);

        modelBuilder.Entity<OrderDetail>()
        .ToTable("OrderDetail")
        .HasOne(b => b.Order)
        .WithMany(d => d.OrderDetails)
        .HasForeignKey(b => b.OrderId);

        modelBuilder.Entity<OrderDetail>()
        .ToTable("OrderDetail")
        .HasOne(b => b.Service)
        .WithMany(d => d.OrderDetails)
        .HasForeignKey(b => b.ServiceId);

        #endregion OrderDetail

        #region Service

        modelBuilder.Entity<Service>()
           .ToTable("Service")
           .HasKey(a => a.Id);

        modelBuilder.Entity<Service>()
          .ToTable("Service")
          .HasOne(b => b.Brand)
          .WithMany(d => d.Services)
          .HasForeignKey(b => b.BrandId);

        modelBuilder.Entity<Service>()
          .ToTable("Service")
          .HasOne(b => b.Sponsor)
          .WithMany(d => d.Services)
          .HasForeignKey(b => b.SponsorId);

        #endregion Service


        _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
