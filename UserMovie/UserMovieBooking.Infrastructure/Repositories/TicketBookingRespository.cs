using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UserMovieBooking.Domain.Models;
using UserMovieBooking.Domain.Repository.Interfaces;
using UserMovieBooking.Infrastructure.Contexts;
using UserMovieBooking.Infrastructure.DataModels;

namespace UserMovieBooking.Infrastructure.Repositories
{
    public class TicketBookingRespository : ITicketBookingRespository
    {
        private readonly MovieManagementContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TicketBookingRespository> _logger;

        public TicketBookingRespository(MovieManagementContext context, IMapper mapper, ILogger<TicketBookingRespository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> AddMovieTicketAsync(TicketBookingModel ticketBookingModel)
        {
            string response = string.Empty;
            try
            {
                var movie = _mapper.Map<Movie>(ticketBookingModel.MovieDetails);
                var ticketDetails = _mapper.Map<TicketBookingDatum>(ticketBookingModel.TicketDetails);
                var userBooking = _mapper.Map<UserBookingDatum>(ticketBookingModel.UserDetails);

                int? movieId = _context.Movies.FirstOrDefault(x => x == movie)?.Id;
                if(movieId == null)
                {
                    response = "Provieded movie deatils do not exist, Please provide valid details.";
                }
                else
                {
                    await _context.UserBookingData.AddAsync(userBooking);
                    await _context.SaveChangesAsync();

                    ticketDetails.UserBookingDataId = userBooking.Id;
                    await _context.TicketBookingData.AddAsync(ticketDetails);
                    await _context.SaveChangesAsync();

                    response = "Ticket details Saved Successfully!";
                }                
            }
            catch (Exception ex)
            {
                response = ex.Message;
                _logger.LogError("Eception Message {Exception}", ex.StackTrace);
            }

            return response;
        }

        public async Task<string> CancelBookedTicketAsync(string ticketNumber)
        {
            var response = string.Empty;
            try
            {
                var ticketBookingData = await _context.TicketBookingData.FirstOrDefaultAsync(x => x.TicketNumber == ticketNumber && !x.IsCancelled);
                if (ticketBookingData != null)
                {
                    ticketBookingData.IsCancelled = true;
                }

                await _context.SaveChangesAsync();
                response = $"Ticket Cancelled Successfully!";
            }
            catch (Exception ex)
            {      
                response = ex.Message;
                _logger.LogError("Eception Message {Exception}", ex.StackTrace);
            }

            return response;
        }

        public async Task<IList<TicketBookingModel>> GetBookedTickedHistoryAsync(string email)
        {
            var response = new List<TicketBookingModel>();
            try
            {
                var bookedTicketDetails = (from tb in _context.TicketBookingData
                                           join ub in _context.UserBookingData
                                           on tb.UserBookingDataId equals ub.Id
                                           join m in _context.Movies
                                           on tb.MovieId equals m.Id
                                           where ub.UserEmail == email
                                           select new TicketBookingModel
                                           {
                                               MovieDetails = _mapper.Map<MovieDetails>(m),
                                               TicketDetails = _mapper.Map<TicketDetails>(tb),
                                               UserDetails = _mapper.Map<UserDetails>(ub)
                                           });

                if(bookedTicketDetails != null)
                {
                    response = await bookedTicketDetails.ToListAsync();
                }
            }
            catch (Exception ex)
            {                
                _logger.LogError("Eception Message {Exception}", ex.StackTrace);
            }

            return response;
        }

        public async Task<TicketBookingModel> GetBookedTicketDetailsAsync(string ticketNumber)
        {
            var response = new TicketBookingModel();
            try
            {
                var bookedTicketDetails = (from tb in _context.TicketBookingData
                                           join ub in _context.UserBookingData
                                           on tb.UserBookingDataId equals ub.Id
                                           join m in _context.Movies
                                           on tb.MovieId equals m.Id
                                           where tb.TicketNumber == ticketNumber
                                           select new TicketBookingModel
                                           {
                                               MovieDetails = _mapper.Map<MovieDetails>(m),
                                               TicketDetails = _mapper.Map<TicketDetails>(tb),
                                               UserDetails = _mapper.Map<UserDetails>(ub)
                                           });

                if (bookedTicketDetails != null)
                {
                    response = await bookedTicketDetails.FirstOrDefaultAsync();

                }
            }
            catch (Exception ex)
            {
                
                _logger.LogError("Eception Message {Exception}", ex.StackTrace);
            }

            return response ?? new TicketBookingModel();
        }

        public async Task<IList<MovieModel>> GetMoviesAsync(string movieName)
        {
            var response = new List<MovieModel>();
            try
            {
                _logger.LogInformation("GetMoviesAsync method started");
                var movieList = await _context.Movies.Where(m =>  m.Name == movieName).ToListAsync();
                if(movieList?.Count > 0)
                {
                    response = _mapper.Map<List<Movie>, List<MovieModel>>(movieList);
                }
                _logger.LogInformation("GetMoviesAsync method ended {response}", response);
            }
            catch (Exception ex)
            {                
                _logger.LogError("Eception Message {Exception}", ex.StackTrace);
            }

            return response ?? new List<MovieModel>();
        }
    }
}
