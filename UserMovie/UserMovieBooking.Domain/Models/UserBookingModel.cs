using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMovieBooking.Domain.Models
{
    public class UserBookingModel
    {
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public int NumberOfSeats { get; set; }
        public int Age { get; set; }
        public string SeatNumbers { get; set; } = string.Empty;
    }
}
