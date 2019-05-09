using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using angular_netcore.Core.Models;
using angular_netcore.Resources;
using AutoMapper;
using react_netcore.Core.Models;

namespace angular_netcore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain API to viewmodel
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
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
                .ForMember(vr => vr.Make,
                    opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.Contact,
                    opt => opt.MapFrom(v => new ContactResource
                        {Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail}))
                .ForMember(vr => vr.Features,
                    opt => opt.MapFrom(v =>
                        v.Features.Select(vf => new KeyValuePairResource
                            {Id = vf.Feature.Id, Name = vf.Feature.Name})));
                

            // API Resource to domain
            CreateMap<ListFilterResource, ListFilter>();
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.ID, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(resource => resource.Contact.Name))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(resource => resource.Contact.Phone))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(resource => resource.Contact.Email))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) =>
                {
                    var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();
                    foreach (var removedFeature in removedFeatures)
                    {
                        v.Features.Remove(removedFeature);
                    }
                    var addedFeatures = vr.Features.Where(id => v.Features.All(vh => vh.FeatureId != id)).Select(fid => new VehicleFeature { FeatureId = fid }).ToList();

                    foreach (var vehicleFeature in addedFeatures)
                    {
                        v.Features.Add(vehicleFeature);
                    }
                });
        }
    }
}