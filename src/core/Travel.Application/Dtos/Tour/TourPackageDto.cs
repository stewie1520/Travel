using AutoMapper;

using Travel.Domain.Entities;
using Travel.Application.Common.Mappings;

namespace Travel.Application.Dtos.Tour
{
    public class TourPackageDto : IMapFrom<TourPackage>
    {
        public int Id { get; set; }
        public string ListId { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public bool InstantConfirmation { get; set; }
        public int Currency { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TourPackage, TourPackageDto>()
                .ForMember(d => d.Currency, opt => opt.MapFrom(s => (int)s.Currency));
        }
    }
}
