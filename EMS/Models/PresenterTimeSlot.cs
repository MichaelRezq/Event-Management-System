using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMS.Models;

namespace EMS.Models
{
    public class PresenterTimeSlot
    {
        public PresenterTimeSlot()
        {
            occupied = false;
        }
        public int Id { get; set; }


        private TimeSpan startTime;

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Prefarred Start Time")]
        [Range(typeof(TimeSpan), "00:00", "22:59")]
        public TimeSpan StartTime
        {
            get { return startTime; }
            set
            {
                if (value >= TimeSpan.Zero && value <= TimeSpan.FromDays(1))
                    startTime = value;
                else
                    throw new ArgumentOutOfRangeException("StartTime", "The Start Time must be between 00:00:00 and 23:59:59.");
            }
        }

        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public TimeSpan EndTime
        {
            get { return StartTime.Add(TimeSpan.FromHours(1)); }
            set { StartTime = value.Subtract(TimeSpan.FromHours(1)); }
        }

        private bool occupied;
        public bool Occupied
        {
            get
            {
                if (DateTime.Now.TimeOfDay >= EndTime) // Check if current time is greater than or equal to EndTime
                    occupied = false; // Set occupied to false
                return occupied;
            }
            set { occupied = value; }
        }

        [Required]
        [Display(Name ="Presenters Sectors")]

        [ForeignKey("PresenterSector")]
        public int PresenterSectorId { get; set; }
        public PresenterSector PresenterSectors { get; set; }
    }
}
