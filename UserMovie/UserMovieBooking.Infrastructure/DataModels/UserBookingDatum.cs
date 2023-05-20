using System;
using System.Collections.Generic;

namespace UserMovieBooking.Infrastructure.DataModels;

public partial class UserBookingDatum
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public int TotalBookedSeats { get; set; }

    public string? SeatNumbers { get; set; }

    public virtual ICollection<TicketBookingDatum> TicketBookingData { get; set; } = new List<TicketBookingDatum>();
}
