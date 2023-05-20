using System;
using System.Collections.Generic;

namespace UserMovieBooking.Infrastructure.DataModels;

public partial class TicketBookingDatum
{
    public int Id { get; set; }

    public int UserBookingDataId { get; set; }

    public int MovieId { get; set; }

    public string TicketNumber { get; set; } = null!;

    public bool IsCancelled { get; set; }

    public decimal TotalPrice { get; set; }
}
