using AMSS.Models;
using AMSS.Models.Dto.Crop;
using AMSS.Models.Dto.CropType;
using AMSS.Models.Dto.Farm;
using AMSS.Models.Dto.Field;
using AMSS.Models.Dto.Location;
using AMSS.Models.Dto.Polygon;
using AMSS.Models.Dto.User;
using AMSS.Models.Polygon;
using AutoMapper;

namespace AMSS.Utility
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                // User
                config.CreateMap<ApplicationUser, UserDto>().ReverseMap();

                // Location
                config.CreateMap<Location, LocationDto>().ReverseMap();
                config.CreateMap<Location, CreateLocationDto>().ReverseMap();

                // Farm
                config.CreateMap<Farm, FarmDto>().ReverseMap();
                config.CreateMap<Farm, CreateFarmDto>().ReverseMap();

                // Field
                config.CreateMap<Field, FieldDto>().ReverseMap();
                config.CreateMap<Field, CreateFieldDto>().ReverseMap();
                config.CreateMap<Field, UpdateFieldDto>().ReverseMap();

                // Crop
                config.CreateMap<Crop, CropDto>().ReverseMap();
                config.CreateMap<Crop, CreateCropDto>().ReverseMap();

                // CropType
                config.CreateMap<CropType, CropTypeDto>().ReverseMap();

                // Polygon 
                config.CreateMap<PolygonApp, PolygonDto>().ReverseMap();
                config.CreateMap<PolygonApp, CreatePolygonDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
