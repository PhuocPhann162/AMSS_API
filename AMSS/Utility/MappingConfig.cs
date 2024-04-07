using AMSS.Models;
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
                config.CreateMap<ApplicationUser, UserDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
