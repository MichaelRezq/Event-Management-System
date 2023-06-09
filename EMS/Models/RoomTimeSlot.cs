using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace EMS.Models
{
    public class RoomTimeSlot
    {
        public RoomTimeSlot()
        {
            occupied = false; // Set the initial value to false
        }
        public int Id { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
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
