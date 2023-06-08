using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace WebApplication1.Models
{
    public class ReservationTimeSlot
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
