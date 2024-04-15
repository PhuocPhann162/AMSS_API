using AMSS.Models;
using AMSS.Models.Dto.Crop;
using AMSS.Models.Dto.Farm;
using AMSS.Models.Dto.Field;
using AMSS.Models.Dto.Location;
using AMSS.Models.Dto.User;
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
                config.CreateMap<Field, Field>().ReverseMap();
                config.CreateMap<Field, CreateFieldDto>().ReverseMap();

                // Crop
                config.CreateMap<Crop, CropDto>().ReverseMap();
                config.CreateMap<Crop, CreateCropDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
