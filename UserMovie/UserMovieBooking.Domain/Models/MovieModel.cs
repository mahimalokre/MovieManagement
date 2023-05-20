using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMovieBooking.Domain.Models
{
    public  class MovieModel
    {
        public string MovieName { get; set; } = string.Empty;
        public string MoviePlace { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }        
    }
}
