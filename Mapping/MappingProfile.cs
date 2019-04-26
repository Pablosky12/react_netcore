using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using angular_netcore.Models;
using angular_netcore.Resources;
using AutoMapper;

namespace angular_netcore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain API to viewmodel
            CreateMap<Make, MakeResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact,
                    opt => opt.MapFrom(v => new ContactResource
                    { Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail }))
                .ForMember(vr => vr.Features,
                    opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Contact,
                    opt => opt.MapFrom(v => new ContactResource
                        {Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail}))
                .ForMember(vr => vr.Features,
                    opt => opt.MapFrom(v =>
                        v.Features.Select(vf => new KeyValuePairResource {Id = vf.Feature.Id, Name = vf.Feature.Name})))
                .ForMember(v => v.Make, opt => opt.MapFrom(vm => vm.Model.Make));

            // API Resource to domain
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.ID, opt => opt.Ignore())
                .ForMember(v => v.ContactName, from => from.MapFrom(resource => resource.Contact.Name))
                .ForMember(v => v.ContactPhone, from => from.MapFrom(resource => resource.Contact.Phone))
                .ForMember(v => v.ContactEmail, from => from.MapFrom(resource => resource.Contact.Email))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) =>
                {
                    var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId));
                    foreach (var removedFeature in removedFeatures)
                    {
                        v.Features.Remove(removedFeature);
                    }
                    var addedFeatures = vr.Features.Where(id => v.Features.All(vh => vh.FeatureId != id)).Select(fid => new VehicleFeature { FeatureId = fid });

                    foreach (var vehicleFeature in addedFeatures)
                    {
                        v.Features.Add(vehicleFeature);
                    }
                });
        }
    }
}