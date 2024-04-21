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
                   Code = "RC"
               },
                new CropType
                {
                    Id = 2,
                    Name = "Corn",
                    Code = "CN"
                },
                new CropType
                {
                    Id = 3,
                    Name = "Soybean",
                    Code = "SB"
                },
                new CropType
                {
                    Id = 4,
                    Name = "Sugarcane",
                    Code = "SC"
                },
                new CropType
                {
                    Id = 5,
                    Name = "Potato",
                    Code = "PT"
                },
                new CropType
                {
                    Id = 6,
                    Name = "Tomato",
                    Code = "TM"
                },
                new CropType
                {
                    Id = 7,
                    Name = "Apple",
                    Code = "AP"
                },
                new CropType
                {
                    Id = 8,
                    Name = "Banana",
                    Code = "BN"
                },
                new CropType
                {
                    Id = 9,
                    Name = "Orange",
                    Code = "OR"
                },
                new CropType
                {
                    Id = 10,
                    Name = "Grapes",
                    Code = "GP"
                },
                new CropType
                {
                    Id = 11,
                    Name = "Pineapple",
                    Code = "PL"
                },
                new CropType
                {
                    Id = 12,
                    Name = "Strawberry",
                    Code = "SBY"
                },
                new CropType
                {
                    Id = 13,
                    Name = "Coffee",
                    Code = "CF"
                },
                new CropType
                {
                    Id = 14,
                    Name = "Tea",
                    Code = "TE"
                },
                new CropType
                {
                    Id = 15,
                    Name = "Cocoa",
                    Code = "CC"
                },
                new CropType
                {
                    Id = 16,
                    Name = "Peanut",
                    Code = "PN"
                },
                new CropType
                {
                    Id = 17,
                    Name = "Carrot",
                    Code = "CR"
                },
                new CropType
                {
                    Id = 18,
                    Name = "Broccoli",
                    Code = "BK"
                }
            );

            modelBuilder.Entity<Crop>().HasData(
                 new Crop
                 {
                     Id = 1,
                     Icon = "https://amssimages.blob.core.windows.net/agriculture/Rice.jpg",
                     Name = "Rice Crop 1",
                     CultivatedArea = 10.5,
                     PlantedDate = DateTime.Now.AddDays(-30),
                     ExpectedDate = DateTime.Now.AddDays(90),
                     Quantity = 500,
                     CropTypeId = 1,
                     FieldId = 7,
                 },
                new Crop
                {
                    Id = 2,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Rice.jpg",
                    Name = "Rice Crop 2",
                    CultivatedArea = 8.2,
                    PlantedDate = DateTime.Now.AddDays(-45),
                    ExpectedDate = DateTime.Now.AddDays(95),
                    Quantity = 400,
                    CropTypeId = 1,
                    FieldId = 3, 
                },

                new Crop
                {
                    Id = 3,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Corn.jpg",
                    Name = "Corn Crop 1",
                    CultivatedArea = 15.7,
                    PlantedDate = DateTime.Now.AddDays(-20),
                    ExpectedDate = DateTime.Now.AddDays(100),
                    Quantity = 700,
                    CropTypeId = 2,
                    FieldId = 5, 
                },
                new Crop
                {
                    Id = 4,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Corn.jpg",
                    Name = "Corn Crop 2",
                    CultivatedArea = 12.3,
                    PlantedDate = DateTime.Now.AddDays(-35),
                    ExpectedDate = DateTime.Now.AddDays(105),
                    Quantity = 600,
                    CropTypeId = 2, 
                    FieldId = 6,
                },
                new Crop
                {
                    Id = 5,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Potato.jpg",
                    Name = "Potato Crop 1",
                    CultivatedArea = 22.5,
                    PlantedDate = DateTime.Now.AddDays(-10),
                    ExpectedDate = DateTime.Now.AddDays(130),
                    Quantity = 1000,
                    CropTypeId = 5,
                    FieldId = 3,
                },
                new Crop
                {
                    Id = 6,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Potato.jpg",
                    Name = "Potato Crop 2",
                    CultivatedArea = 18.7,
                    PlantedDate = DateTime.Now.AddDays(-25),
                    ExpectedDate = DateTime.Now.AddDays(135),
                    Quantity = 900,
                    CropTypeId = 5,
                    FieldId = 6,
                },
                new Crop
                {
                    Id = 7,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Tomato.jpg",
                    Name = "Tomato Crop 1",
                    CultivatedArea = 24.1,
                    PlantedDate = DateTime.Now.AddDays(-5),
                    ExpectedDate = DateTime.Now.AddDays(140),
                    Quantity = 1100,
                    CropTypeId = 6,
                    FieldId = 5,
                },
                new Crop
                {
                    Id = 8,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Tomato.jpg",
                    Name = "Tomato Crop 2",
                    CultivatedArea = 20.5,
                    PlantedDate = DateTime.Now.AddDays(-20),
                    ExpectedDate = DateTime.Now.AddDays(145),
                    Quantity = 1000,
                    CropTypeId = 6,
                    FieldId = 6,
                },
                new Crop
                {
                    Id = 9,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Apple.jpg",
                    Name = "Apple Crop 1",
                    CultivatedArea = 26.3,
                    PlantedDate = DateTime.Now.AddDays(5),
                    ExpectedDate = DateTime.Now.AddDays(150),
                    Quantity = 1200,
                    CropTypeId = 7,
                    FieldId = 3,
                },
                new Crop
                {
                    Id = 10,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Apple.jpg",
                    Name = "Apple Crop 2",
                    CultivatedArea = 22.8,
                    PlantedDate = DateTime.Now.AddDays(-10),
                    ExpectedDate = DateTime.Now.AddDays(155),
                    Quantity = 1100,
                    CropTypeId = 7,
                    FieldId = 7,
                },
                new Crop
                {
                    Id = 11,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Banana.jpg",
                    Name = "Banana Crop 1",
                    CultivatedArea = 28.6,
                    PlantedDate = DateTime.Now.AddDays(10),
                    ExpectedDate = DateTime.Now.AddDays(160),
                    Quantity = 1300,
                    CropTypeId = 8,
                    FieldId = 5,
                },
                new Crop
                {
                    Id = 12,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Banana.jpg",
                    Name = "Banana Crop 2",
                    CultivatedArea = 24.9,
                    PlantedDate = DateTime.Now.AddDays(-5),
                    ExpectedDate = DateTime.Now.AddDays(165),
                    Quantity = 1200,
                    CropTypeId = 8,
                    FieldId = 3,
                },
                new Crop
                {
                    Id = 13,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Orange.jpg",
                    Name = "Orange Crop 1",
                    CultivatedArea = 30.9,
                    PlantedDate = DateTime.Now.AddDays(15),
                    ExpectedDate = DateTime.Now.AddDays(170),
                    Quantity = 1400,
                    CropTypeId = 9,
                    FieldId = 6,
                },
                new Crop
                {
                    Id = 14,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Orange.jpg",
                    Name = "Orange Crop 2",
                    CultivatedArea = 26.7,
                    PlantedDate = DateTime.Now.AddDays(0),
                    ExpectedDate = DateTime.Now.AddDays(175),
                    Quantity = 1300,
                    CropTypeId = 9,
                    FieldId = 7,
                },
                new Crop
                {
                    Id = 15,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Grape.jpg",
                    Name = "Grapes Crop 1",
                    CultivatedArea = 32.4,
                    PlantedDate = DateTime.Now.AddDays(20),
                    ExpectedDate = DateTime.Now.AddDays(180),
                    Quantity = 1500,
                    CropTypeId = 10,
                    FieldId = 5,
                },
                new Crop
                {
                    Id = 16,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Grape.jpg",
                    Name = "Grapes Crop 2",
                    CultivatedArea = 28.1,
                    PlantedDate = DateTime.Now.AddDays(5),
                    ExpectedDate = DateTime.Now.AddDays(185),
                    Quantity = 1400,
                    CropTypeId = 10,
                    FieldId = 6,
                },
                new Crop
                {
                    Id = 17,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Pineapple.jpg",
                    Name = "Pineapple Crop 1",
                    CultivatedArea = 34.7,
                    PlantedDate = DateTime.Now.AddDays(25),
                    ExpectedDate = DateTime.Now.AddDays(190),
                    Quantity = 1600,
                    CropTypeId = 11, 
                    FieldId = 3,
                },
                new Crop
                {
                    Id = 18,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Pineapple.jpg",
                    Name = "Pineapple Crop 2",
                    CultivatedArea = 30.5,
                    PlantedDate = DateTime.Now.AddDays(10),
                    ExpectedDate = DateTime.Now.AddDays(195),
                    Quantity = 1500,
                    CropTypeId = 11,
                    FieldId = 6,
                },
                new Crop
                {
                    Id = 19,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Strawberry.jpg",
                    Name = "Strawberry Crop 1",
                    CultivatedArea = 36.2,
                    PlantedDate = DateTime.Now.AddDays(30),
                    ExpectedDate = DateTime.Now.AddDays(200),
                    Quantity = 1700,
                    CropTypeId = 12,
                    FieldId = 5,
                },
                new Crop
                {
                    Id = 20,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Strawberry.jpg",
                    Name = "Strawberry Crop 2",
                    CultivatedArea = 32.0,
                    PlantedDate = DateTime.Now.AddDays(15),
                    ExpectedDate = DateTime.Now.AddDays(205),
                    Quantity = 1600,
                    CropTypeId = 12,
                    FieldId = 7,
                },
                new Crop
                {
                    Id = 21,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Coffee.jpg",
                    Name = "Coffee Crop 1",
                    CultivatedArea = 38.0,
                    PlantedDate = DateTime.Now.AddDays(35),
                    ExpectedDate = DateTime.Now.AddDays(210),
                    Quantity = 1800,
                    CropTypeId = 13,
                    FieldId = 5,
                },
                new Crop
                {
                    Id = 22,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Coffee.jpg",
                    Name = "Coffee Crop 2",
                    CultivatedArea = 34.0,
                    PlantedDate = DateTime.Now.AddDays(20),
                    ExpectedDate = DateTime.Now.AddDays(215),
                    Quantity = 1700,
                    CropTypeId = 13,
                    FieldId = 3,
                },
                new Crop
                {
                    Id = 23,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Cocoa.jpg",
                    Name = "Cocoa Crop 1",
                    CultivatedArea = 42.3,
                    PlantedDate = DateTime.Now.AddDays(45),
                    ExpectedDate = DateTime.Now.AddDays(230),
                    Quantity = 2000,
                    CropTypeId = 15,
                    FieldId = 6,
                },
                new Crop
                {
                    Id = 24,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Cocoa.jpg",
                    Name = "Cocoa Crop 2",
                    CultivatedArea = 38.6,
                    PlantedDate = DateTime.Now.AddDays(30),
                    ExpectedDate = DateTime.Now.AddDays(235),
                    Quantity = 1900,
                    CropTypeId = 15,
                    FieldId = 5,
                },
                new Crop
                {
                    Id = 25,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Peanut.jpg",
                    Name = "Peanut Crop 1",
                    CultivatedArea = 44.7,
                    PlantedDate = DateTime.Now.AddDays(50),
                    ExpectedDate = DateTime.Now.AddDays(240),
                    Quantity = 2100,
                    CropTypeId = 16,
                    FieldId = 7,
                },
                new Crop
                {
                    Id = 26,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Peanut.jpg",
                    Name = "Peanut Crop 2",
                    CultivatedArea = 40.4,
                    PlantedDate = DateTime.Now.AddDays(35),
                    ExpectedDate = DateTime.Now.AddDays(245),
                    Quantity = 2000,
                    CropTypeId = 16,
                    FieldId = 5,
                },
                new Crop
                {
                    Id = 27,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Carrot.jpg",
                    Name = "Carrot Crop 1",
                    CultivatedArea = 46.9,
                    PlantedDate = DateTime.Now.AddDays(55),
                    ExpectedDate = DateTime.Now.AddDays(250),
                    Quantity = 2200,
                    CropTypeId = 17,
                    FieldId = 6,
                },
                new Crop
                {
                    Id = 28,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Carrot.jpg",
                    Name = "Carrot Crop 2",
                    CultivatedArea = 42.6,
                    PlantedDate = DateTime.Now.AddDays(40),
                    ExpectedDate = DateTime.Now.AddDays(255),
                    Quantity = 2100,
                    CropTypeId = 17,
                    FieldId = 3,
                },
                new Crop
                {
                    Id = 29,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Brocolli.jpg",
                    Name = "Broccoli Crop 1",
                    CultivatedArea = 48.1,
                    PlantedDate = DateTime.Now.AddDays(60),
                    ExpectedDate = DateTime.Now.AddDays(260),
                    Quantity = 2300,
                    CropTypeId = 18,
                    FieldId = 7,
                },
                new Crop
                {
                    Id = 30,
                    Icon = "https://amssimages.blob.core.windows.net/agriculture/Brocolli.jpg",
                    Name = "Broccoli Crop 2",
                    CultivatedArea = 44.0,
                    PlantedDate = DateTime.Now.AddDays(45),
                    ExpectedDate = DateTime.Now.AddDays(265),
                    Quantity = 2200,
                    CropTypeId = 18,
                    FieldId = 5,
                }
            );
        }
    }
}
