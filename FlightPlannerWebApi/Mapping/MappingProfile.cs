using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlannerWebApi.Models;

namespace FlightPlannerWebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Airport, AirportViewModel>()
                .ForMember(viewModel => viewModel.Airport,
                    options => options
                        .MapFrom(source => source.AirportCode));
            CreateMap<AirportViewModel, Airport>()
                .ForMember(destination => destination.AirportCode,
                    options => options
                        .MapFrom(source => source.Airport));

            CreateMap<AddFlightRequest, Flight>()
                .ForMember(destination => destination.Id, 
                    options => options.Ignore())
                .ForMember(destination => destination.FromId,
                    options => options.Ignore())
                .ForMember(destination => destination.ToId,
                    options => options.Ignore());
            CreateMap<Flight, AddFlightResponse>();
        }
    }
}
