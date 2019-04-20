using AutoMapper;

namespace angular_netcore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Make, ViewModels.Make>();
            CreateMap<Models.Model, ViewModels.Model>();
            CreateMap<Models.Feature, ViewModels.Feature>();
        }
    }
}