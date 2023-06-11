using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Models
{
    public class RoomTimeSlot
    {
        public RoomTimeSlot()
        {
            occupied = false; // Set the initial value to false
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
       // [Range(typeof(TimeSpan), "00:00:00", "22:59:00")]
        public TimeSpan StartTime { get; set; }

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

        [ForeignKey("ConferenceRoom")]
        public int ConferenceRoomId { get; set; }
        public ConferenceRoom ConferenceRoom { get; set; }
    }
}
