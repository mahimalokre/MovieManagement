using System;
using System.Collections.Generic;

namespace AdminMovieBooking.Infrastructure.DataModels;

public partial class User
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
}
