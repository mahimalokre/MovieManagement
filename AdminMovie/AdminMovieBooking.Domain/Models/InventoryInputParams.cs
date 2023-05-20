using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMovieBooking.Domain.Models
{
    public class InventoryInputParams
    {
        [DisplayName("Movie Name")]
        public string Name { get; set; } = null!;

        public string Place { get; set; } = null!;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string ScheduledDays { get; set; } = null!;

        public string ShowType { get; set; } = null!;

        public bool IsBlocked { get; set; }

        public decimal VipSeatCost { get; set; }

        public decimal VipSeatTax { get; set; }

        public decimal NonVipSeatCost { get; set; }

        public decimal NonVipSeatTax { get; set; }

        public int TotalRows { get; set; }

        public int VipRows { get; set; }

        public int TotalSeatsEachRow { get; set; }
    }
}
