using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMovieBooking.Domain.Models;
using UserMovieBooking.Infrastructure.DataModels;

namespace UserMovieBooking.Infrastructure.Mapper
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<Movie, MovieModel>()
                .ForMember(x => x.MovieName, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.MoviePlace, y => y.MapFrom(z => z.Place));
            CreateMap<MovieModel, Movie>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.MovieName))
                .ForMember(x => x.Place, y => y.MapFrom(z => z.MoviePlace));
            
            CreateMap<UserDetails, UserBookingDatum>();
            CreateMap<MovieDetails, Movie>();
            CreateMap<TicketDetails, TicketBookingDatum>();

            CreateMap<UserBookingDatum, UserDetails>();
            CreateMap<Movie, MovieDetails>();
            CreateMap<TicketBookingDatum, TicketDetails>();
        }
    }
}


