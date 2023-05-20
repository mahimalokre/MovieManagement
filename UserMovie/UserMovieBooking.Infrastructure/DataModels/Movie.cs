using System;
using System.Collections.Generic;

namespace UserMovieBooking.Infrastructure.DataModels;

public partial class Movie
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Place { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string ScheduledDays { get; set; } = null!;

    public string ShowType { get; set; } = null!;

    public bool IsBlocked { get; set; }
}
