using AdminMovieBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMovieBooking.Domain.Repository.Interfaces
{
    public interface IInventoryRepository
    {
        Task<string> AddMovieInventoryDetailsAsync(InventoryInputParams inventoryInputParams);
    }
}
