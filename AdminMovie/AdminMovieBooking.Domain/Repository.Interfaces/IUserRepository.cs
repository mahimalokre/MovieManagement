using AdminMovieBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMovieBooking.Domain.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<string> RegisterUserAsync(UserModel user);
        Task<string> LoginUserAsync(UserModel user);
    }
}
