﻿using AMSS.Models;
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
        public DbSet<SoilQuality> SoilQualitys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SoilQuality>().HasData(
                new SoilQuality
                {
                    Id = 1,
                    InfoTime = DateTime.Now,
                    Chlorophyll = 0.1f,
                    Iron = 0.2f,
                    Nitrate = 0.3f,
                    Phyto = 0.4f,
                    Oxygen = 0.5f,
                    PH = 7.0f,
                    Phytoplankton = 0.6f,
                    Silicate = 0.7f,
                    Salinity = 0.8f,
                    SoilMoisture = 50,
                    SoilMoisture10cm = 60,
                    SoilMoisture40cm = 70,
                    SoilMoisture100cm = 80,
                    SoilTemperature = 25,
                    SoilTemperature10cm = 26,
                    SoilTemperature40cm = 27,
                    SoilTemperature100cm = 28,
                    FieldId = 3
                },
                new SoilQuality
                {
                    Id = 2,
                    InfoTime = DateTime.Now,
                    Chlorophyll = 0.2f,
                    Iron = 0.3f,
                    Nitrate = 0.4f,
                    Phyto = 0.5f,
                    Oxygen = 0.6f,
                    PH = 7.1f,
                    Phytoplankton = 0.7f,
                    Silicate = 0.8f,
                    Salinity = 0.9f,
                    SoilMoisture = 55,
                    SoilMoisture10cm = 65,
                    SoilMoisture40cm = 75,
                    SoilMoisture100cm = 85,
                    SoilTemperature = 26,
                    SoilTemperature10cm = 27,
                    SoilTemperature40cm = 28,
                    SoilTemperature100cm = 29,
                    FieldId = 5
                },
                new SoilQuality
                {
                    Id = 3,
                    InfoTime = DateTime.Now.AddDays(-1),
                    Chlorophyll = 0.15f,
                    Iron = 0.25f,
                    Nitrate = 0.35f,
                    Phyto = 0.45f,
                    Oxygen = 0.55f,
                    PH = 7.2f,
                    Phytoplankton = 0.65f,
                    Silicate = 0.75f,
                    Salinity = 0.85f,
                    SoilMoisture = 52,
                    SoilMoisture10cm = 62,
                    SoilMoisture40cm = 72,
                    SoilMoisture100cm = 82,
                    SoilTemperature = 24,
                    SoilTemperature10cm = 25,
                    SoilTemperature40cm = 26,
                    SoilTemperature100cm = 27,
                    FieldId = 6
                },
                new SoilQuality
                {
                    Id = 4,
                    InfoTime = DateTime.Now.AddDays(-2),
                    Chlorophyll = 0.12f,
                    Iron = 0.22f,
                    Nitrate = 0.32f,
                    Phyto = 0.42f,
                    Oxygen = 0.52f,
                    PH = 7.3f,
                    Phytoplankton = 0.62f,
                    Silicate = 0.72f,
                    Salinity = 0.82f,
                    SoilMoisture = 53,
                    SoilMoisture10cm = 63,
                    SoilMoisture40cm = 73,
                    SoilMoisture100cm = 83,
                    SoilTemperature = 23,
                    SoilTemperature10cm = 24,
                    SoilTemperature40cm = 25,
                    SoilTemperature100cm = 26,
                    FieldId = 7
                },
                new SoilQuality
                {
                    Id = 5,
                    InfoTime = DateTime.Now.AddDays(-3),
                    Chlorophyll = 0.11f,
                    Iron = 0.21f,
                    Nitrate = 0.31f,
                    Phyto = 0.41f,
                    Oxygen = 0.51f,
                    PH = 7.4f,
                    Phytoplankton = 0.61f,
                    Silicate = 0.71f,
                    Salinity = 0.81f,
                    SoilMoisture = 54,
                    SoilMoisture10cm = 64,
                    SoilMoisture40cm = 74,
                    SoilMoisture100cm = 84,
                    SoilTemperature = 22,
                    SoilTemperature10cm = 23,
                    SoilTemperature40cm = 24,
                    SoilTemperature100cm = 25,
                    FieldId = 8
                },
                 new SoilQuality
                 {
                     Id = 6,
                     InfoTime = DateTime.Now.AddDays(-4),
                     Chlorophyll = 0.13f,
                     Iron = 0.23f,
                     Nitrate = 0.33f,
                     Phyto = 0.43f,
                     Oxygen = 0.53f,
                     PH = 7.5f,
                     Phytoplankton = 0.63f,
                     Silicate = 0.73f,
                     Salinity = 0.83f,
                     SoilMoisture = 56,
                     SoilMoisture10cm = 66,
                     SoilMoisture40cm = 76,
                     SoilMoisture100cm = 86,
                     SoilTemperature = 21,
                     SoilTemperature10cm = 22,
                     SoilTemperature40cm = 23,
                     SoilTemperature100cm = 24,
                     FieldId = 9
                 },
                 new SoilQuality
                 {
                     Id = 7,
                     InfoTime = DateTime.Now.AddDays(-5),
                     Chlorophyll = 0.14f,
                     Iron = 0.24f,
                     Nitrate = 0.34f,
                     Phyto = 0.44f,
                     Oxygen = 0.54f,
                     PH = 7.6f,
                     Phytoplankton = 0.64f,
                     Silicate = 0.74f,
                     Salinity = 0.84f,
                     SoilMoisture = 57,
                     SoilMoisture10cm = 67,
                     SoilMoisture40cm = 77,
                     SoilMoisture100cm = 87,
                     SoilTemperature = 20,
                     SoilTemperature10cm = 21,
                     SoilTemperature40cm = 22,
                     SoilTemperature100cm = 23,
                     FieldId = 10
                 },
                new SoilQuality
                {
                    Id = 8,
                    InfoTime = DateTime.Now.AddDays(-6),
                    Chlorophyll = 0.16f,
                    Iron = 0.26f,
                    Nitrate = 0.36f,
                    Phyto = 0.46f,
                    Oxygen = 0.56f,
                    PH = 7.7f,
                    Phytoplankton = 0.66f,
                    Silicate = 0.76f,
                    Salinity = 0.86f,
                    SoilMoisture = 58,
                    SoilMoisture10cm = 68,
                    SoilMoisture40cm = 78,
                    SoilMoisture100cm = 88,
                    SoilTemperature = 19,
                    SoilTemperature10cm = 20,
                    SoilTemperature40cm = 21,
                    SoilTemperature100cm = 22,
                    FieldId = 11
                });

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
                     Description = "High-yielding rice variety suitable for wetland cultivation.",
                     CultivatedArea = 891.0,
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
                    Description = "High-yielding rice variety suitable for wetland cultivation.",
                    CultivatedArea = 900.2,
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
                    Description = "Maize crop known for its versatility and use in various food products.",
                    CultivatedArea = 667.7,
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
                    Description = "Maize crop known for its versatility and use in various food products.",
                    CultivatedArea = 292.3,
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
                    Description = "Starchy tuberous crop widely consumed as a staple food.",
                    CultivatedArea = 729.4,
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
                    Description = "Starchy tuberous crop widely consumed as a staple food.",
                    CultivatedArea = 834.0,
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
                    Description = "Versatile crop used in various culinary dishes and sauces, rich in vitamins and minerals.",
                    CultivatedArea = 662.4,
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
                    Description = "Versatile crop used in various culinary dishes and sauces, rich in vitamins and minerals.",
                    CultivatedArea = 389.0,
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
                    Description = "Popular fruit crop known for its crisp texture and sweet flavor, cultivated in orchards worldwide.",
                    CultivatedArea = 732.1,
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
                    Description = "Popular fruit crop known for its crisp texture and sweet flavor, cultivated in orchards worldwide.",
                    CultivatedArea = 424.6,
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
                    Description = "Tropical fruit crop with high nutritional value, grown in warm climates and consumed worldwide.",
                    CultivatedArea = 293.4,
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
                    Description = "Tropical fruit crop with high nutritional value, grown in warm climates and consumed worldwide.",
                    CultivatedArea = 444.5,
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
                    Description = "Citrus fruit crop prized for its tangy flavor and high vitamin C content, cultivated in orchards.",
                    CultivatedArea = 920.1,
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
                    Description = "Citrus fruit crop prized for its tangy flavor and high vitamin C content, cultivated in orchards.",
                    CultivatedArea = 349.5,
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
                    Description = "Fruit crop grown in vineyards, used for making wine, juices, and various culinary products.",
                    CultivatedArea = 542.0,
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
                    Description = "Fruit crop grown in vineyards, used for making wine, juices, and various culinary products.",
                    CultivatedArea = 1300.3,
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
                    Description = "Tropical fruit crop known for its sweet and tangy flavor, grown in tropical regions worldwide.",
                    CultivatedArea = 1250.6,
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
                    Description = "Tropical fruit crop known for its sweet and tangy flavor, grown in tropical regions worldwide.",
                    CultivatedArea = 1332.6,
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
                    Description = "Small fruit crop with a sweet flavor, often grown in gardens and commercial fields for fresh consumption.",
                    CultivatedArea = 1330.5,
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
                    Description = "Small fruit crop with a sweet flavor, often grown in gardens and commercial fields for fresh consumption.",
                    CultivatedArea = 1232.7,
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
                    Description = "Caffeinated beverage crop cultivated in tropical regions, harvested for its seeds (coffee beans).",
                    CultivatedArea = 756.4,
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
                    Description = "Caffeinated beverage crop cultivated in tropical regions, harvested for its seeds (coffee beans).",
                    CultivatedArea = 842.1,
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
                    Description = "Bean crop used in chocolate production, cultivated in tropical regions within 20 degrees of the equator.",
                    CultivatedArea = 542.3,
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
                    Description = "Bean crop used in chocolate production, cultivated in tropical regions within 20 degrees of the equator.",
                    CultivatedArea = 638.6,
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
                    Description = "Legume crop grown for its edible seeds, rich in protein and healthy fats, cultivated in sandy soils.",
                    CultivatedArea = 447.7,
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
                    Description = "Legume crop grown for its edible seeds, rich in protein and healthy fats, cultivated in sandy soils.",
                    CultivatedArea = 1140.4,
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
                    Description = "Root vegetable crop known for its bright orange color and high beta-carotene content, grown in cool climates.",
                    CultivatedArea = 346.9,
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
                    Description = "Root vegetable crop known for its bright orange color and high beta-carotene content, grown in cool climates.",
                    CultivatedArea = 342.6,
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
                    Description = "Nutrient-rich vegetable crop belonging to the brassica family, known for its green flowering heads.",
                    CultivatedArea = 748.1,
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
                    Description = "Nutrient-rich vegetable crop belonging to the brassica family, known for its green flowering heads.",
                    CultivatedArea = 844.0,
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
