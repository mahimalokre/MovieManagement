using System;
using System.Collections.Generic;

namespace AdminMovieBooking.Infrastructure.DataModels;

public partial class MovieSeatConfiguration
{
    public int Id { get; set; }

    public decimal VipSeatCost { get; set; }

    public decimal VipSeatTax { get; set; }

    public decimal NonVipSeatCost { get; set; }

    public decimal NonVipSeatTax { get; set; }
}
