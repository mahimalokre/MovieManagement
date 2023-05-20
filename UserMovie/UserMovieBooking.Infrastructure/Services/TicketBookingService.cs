using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMovieBooking.Domain.Models;
using UserMovieBooking.Domain.Repository.Interfaces;
using UserMovieBooking.Domain.Services.Interfaces;

namespace UserMovieBooking.Infrastructure.Services
{
    public class TicketBookingService : ITicketBookingService
    {
        private readonly ITicketBookingRespository _ticketBookingRespository;
        public TicketBookingService(ITicketBookingRespository ticketBookingRespository) 
        {
            _ticketBookingRespository = ticketBookingRespository;
        }

        public async Task<string> AddMovieTicketAsync(TicketBookingModel ticketBookingModel)
        {
            return await _ticketBookingRespository.AddMovieTicketAsync(ticketBookingModel);
        }

        public async Task<string> CancelBookedTicketAsync(string ticketNumber)
        {
            return await _ticketBookingRespository.CancelBookedTicketAsync(ticketNumber);
        }

        public async Task<IList<TicketBookingModel>> GetBookedTickedHistoryAsync(string email)
        {
            return await _ticketBookingRespository.GetBookedTickedHistoryAsync(email);
        }

        public async Task<TicketBookingModel> GetBookedTicketDetailsAsync(string ticketNumber)
        {
            return await _ticketBookingRespository.GetBookedTicketDetailsAsync(ticketNumber);
        }

        public async Task<IList<MovieModel>> GetMoviesAsync(string movieName)
        {
            return await _ticketBookingRespository.GetMoviesAsync(movieName);
        }
    }
}
