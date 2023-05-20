using AdminMovieBooking.Domain.Models;
using AdminMovieBooking.Domain.Repository.Interfaces;
using AdminMovieBooking.Infrastructure.Contexts;
using AdminMovieBooking.Infrastructure.DataModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMovieBooking.Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly MovieManagementContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public InventoryRepository(MovieManagementContext context, IMapper mapper, ILogger<InventoryRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> AddMovieInventoryDetailsAsync(InventoryInputParams inventoryInputParams)
        {
            string response = string.Empty;
            try
            {
                bool isMovieExist = await _context.Movies.AnyAsync(m => m.Name == inventoryInputParams.Name);
                if (!isMovieExist)
                {
                    var movie = _mapper.Map<Movie>(inventoryInputParams);
                    var movieSeatConfig = _mapper.Map<MovieSeatConfiguration>(inventoryInputParams);
                    var theaterConfig = _mapper.Map<TheaterConfiguration>(inventoryInputParams);

                    var movieAsync = _context.Movies.AddAsync(movie);
                    var movieSeatConfigAsync = _context.MovieSeatConfigurations.AddAsync(movieSeatConfig);
                    var theaterConfigAsync = _context.TheaterConfigurations.AddAsync(theaterConfig);

                    await movieAsync;
                    await movieSeatConfigAsync;
                    await theaterConfigAsync;                    

                    response = $"Movie Saved Successfully";
                }
                else
                {
                    var movie = _mapper.Map<Movie>(inventoryInputParams);
                    await _context.Movies.AddAsync(movie);

                    response = $"Movie details updated successfully !";
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response = ex.Message;
                _logger.LogError("Eception Message {Exception}", ex.Message);
            }

            return response;
        }        
    }
}
