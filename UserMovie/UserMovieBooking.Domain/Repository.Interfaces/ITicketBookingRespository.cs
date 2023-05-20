using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMovieBooking.Domain.Models;

namespace UserMovieBooking.Domain.Repository.Interfaces
{
    public interface ITicketBookingRespository
    {
        Task<IList<MovieModel>> GetMoviesAsync(string movieName);
        Task<string> AddMovieTicketAsync(TicketBookingModel ticketBookingModel);
        Task<TicketBookingModel> GetBookedTicketDetailsAsync(string ticketNumber);
        Task<IList<TicketBookingModel>> GetBookedTickedHistoryAsync(string email);
        Task<string> CancelBookedTicketAsync(string ticketNumber);
    }
}
