using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMovieBooking.Domain.Models
{
    public class SearchInputParams
    {
        public string MovieName { get; set; } = string.Empty;
        public DateTime MovieDate { get; set; }
        public DateTime MovieTime { get; set; }
        public string FromPlace { get; set; } = string.Empty;
        public string ToPlace { get; set; } = string.Empty;
    }
}
