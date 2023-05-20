using System;
using System.Collections.Generic;

namespace AdminMovieBooking.Infrastructure.DataModels;

public partial class TheaterConfiguration
{
    public int Id { get; set; }

    public int TotalRows { get; set; }

    public int VipRows { get; set; }

    public int TotalSeatsEachRow { get; set; }
}
