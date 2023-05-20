using AdminMovieBooking.Domain.Models;
using AdminMovieBooking.Domain.Repository.Interfaces;
using AdminMovieBooking.Infrastructure.Contexts;
using AdminMovieBooking.Infrastructure.DataModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMovieBooking.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieManagementContext _context;
        private readonly IMapper _mapper;

        public UserRepository(MovieManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> RegisterUserAsync(UserModel userModel)
        {
            if (userModel != null)
            {
                var userData = _mapper.Map<User>(userModel);

                var isUserAvailable = await this.IsUserAvailableAsync(userData);
                if (!isUserAvailable)
                {
                    _context.Users.Add(userData);
                    await _context.SaveChangesAsync();

                    return "User saved successfully !!";
                }
                else
                {
                    return $"{userModel.Login} is already exist.";
                }
            }

            return "User data is empty";
        }

        public async Task<string> LoginUserAsync(UserModel userModel)
        {
            if (userModel != null)
            {
                var userData = _mapper.Map<User>(userModel);
                var userName = userData.Login;
                var isUserAvailable = await this.IsUserAvailableAsync(userData);
                if (isUserAvailable)
                {
                    var isPasswordMatch = _context.Users.AnyAsync(u => u.Login == userName && u.Password == userModel.Password).Result;
                    return isPasswordMatch ? "User Login successful" : $"{userName} password did not match";
                }

                return $"No user {userName} found";
            }

            return "User data is empty";
        }

        private async Task<bool> IsUserAvailableAsync(User user)
        {
            return await _context.Users.AnyAsync(u => u.Login == user.Login);
        }
    }
}
