using AMSS.Models;
using AMSS.Models.Polygon;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AMSS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PolygonApp> PolygonApps { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<CropType> CropTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CropType>().HasData(
                new CropType
                {
                    Id = 1,
                    Name = "Rice",
                    Code = "RC",
                },
                new CropType
                {
                    Id = 2,
                    Name = "Corn",
                    Code = "CN",
                },
                new CropType
                {
                    Id = 3,
                    Name = "Soybean",
                    Code = "SB",
                },
                new CropType
                {
                    Id = 4,
                    Name = "Sugarcane",
                    Code = "SG",
                },
                new CropType
                {
                    Id = 5,
                    Name = "Coffee",
                    Code = "CF",
                }
            );

            modelBuilder.Entity<Crop>().HasData(
                 new Crop
                 {
                     Id = 1,
                     Icon = "rice_icon.png",
                     Name = "Long Grain Rice",
                     CultivatedArea = 100.5,
                     PlantedDate = new DateTime(2024, 4, 1),
                     ExpectedDate = new DateTime(2024, 9, 1),
                     Quantity = 500,
                     CropTypeId = 1, 
                     CreatedAt = DateTime.UtcNow,
                     UpdatedAt = DateTime.UtcNow
                 },
                 new Crop
                 {
                     Id = 2,
                     Icon = "corn_icon.png",
                     Name = "Yellow Corn",
                     CultivatedArea = 75.2,
                     PlantedDate = new DateTime(2024, 4, 15),
                     ExpectedDate = new DateTime(2024, 8, 15),
                     Quantity = 300,
                     CropTypeId = 2, 
                     CreatedAt = DateTime.UtcNow,
                     UpdatedAt = DateTime.UtcNow
                 }
            );
        }
    }
}
