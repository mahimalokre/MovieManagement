using AdminMovieBooking.Domain.Models;
using AdminMovieBooking.Domain.Repository.Interfaces;
using AdminMovieBooking.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMovieBooking.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<string> RegisterUserAsync(UserModel user)
        {
            return _userRepository.RegisterUserAsync(user);
        }

        public Task<string> LoginUserAsync(UserModel user)
        {
            return _userRepository.LoginUserAsync(user);
        }
    }
}
