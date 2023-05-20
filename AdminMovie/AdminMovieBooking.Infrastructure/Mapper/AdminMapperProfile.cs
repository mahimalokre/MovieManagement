using AdminMovieBooking.Domain.Models;
using AdminMovieBooking.Infrastructure.DataModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMovieBooking.Infrastructure.Mapper
{
    public class AdminMapperProfile : Profile
    {
        public AdminMapperProfile() 
        {
            CreateMap<InventoryInputParams, Movie>();
            CreateMap<InventoryInputParams, MovieSeatConfiguration>();
            CreateMap<InventoryInputParams, TheaterConfiguration>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();   
        }
    }
}
