using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMovieBooking.Domain.Models
{
    public class TicketBookingModel
    {
        public UserDetails UserDetails { get; set; } = null!;      
        public MovieDetails MovieDetails { get; set; } = null!;
        public TicketDetails TicketDetails { get; set; } = null!;   
    }

    public class UserDetails
    {
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public int NumberOfSeats { get; set; }
        public int Age { get; set; }
        public string SeatNumbers { get; set; } = string.Empty;
    }

    public class MovieDetails
    {
        public string Name { get; set; } = null!;

        public string Place { get; set; } = null!;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string ScheduledDays { get; set; } = null!;

        public string ShowType { get; set; } = null!;
    }

    public class TicketDetails
    {
        public string TicketNumber { get; set; } = string.Empty;
        public bool IsCancelled { get; set; }
        public decimal TotalPrice { get; set; }
    }
    
}
