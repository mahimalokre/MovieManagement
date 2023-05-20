using AdminMovieBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMovieBooking.Domain.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<string> AddMovieInventoryDetailsAsync(InventoryInputParams inventoryInputParams);
    }
}
