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
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<string> AddMovieInventoryDetailsAsync(InventoryInputParams inventoryInputParams)
        {
            return await _inventoryRepository.AddMovieInventoryDetailsAsync(inventoryInputParams);
        }
    }
}
