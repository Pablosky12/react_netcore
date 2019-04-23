using System.Linq;
using angular_netcore.Models;
using angular_netcore.ViewModels;
using AutoMapper;

namespace angular_netcore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain API to viewmodel
            CreateMap<Make, MakeViewModel>();
            CreateMap<Model, ModelViewModel>();
            CreateMap<Feature, FeatureViewModel>();
            CreateMap<Vehicle, VehicleViewModel>()
                .ForMember(vr => vr.Contact,
                    opt => opt.MapFrom(v => new ContactResource
                        {Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail}))
                .ForMember(vr => vr.Features,
                    opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            // API Resource to domain
            CreateMap<VehicleViewModel, Vehicle>()
                .ForMember(v => v.ContactName, from => from.MapFrom(resource => resource.Contact.Name))
                .ForMember(v => v.ContactPhone, from => from.MapFrom(resource => resource.Contact.Phone))
                .ForMember(v => v.ContactEmail, from => from.MapFrom(resource => resource.Contact.Email))
                .ForMember(v => v.Features,
                    from => from.MapFrom(resource => resource.Features.Select(id => new VehicleFeature { FeatureId = id })));
        }
    }
}