using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models
{
    public class Reservation
    {
        public Reservation()
        {
            IsDone = false;
        }

        [Key]
        public int Id { get; set; }

        [AllowNull]
        public string Topic { get; set; }

        [ForeignKey("Presenter")]
        public int PresenterId { get; set; }
        public Presenter Presenter { get; set; }

        [ForeignKey("Investor")]
        public int InvestorId { get; set; }
        public Investor Investor { get; set; }

        [ForeignKey("ConferenceRoom")]
        public int ConferenceRoomId { get; set; }
        public ConferenceRoom ConferenceRooms { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; } = DateTime.Now;

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

        private bool isDone;
        public bool IsDone
        {
            get
            {
                if (DateTime.Now.TimeOfDay >= EndTime) // Check if current time is greater than or equal to EndTime
                    isDone = true;
                return isDone;
            }
            set { isDone = value; }
        }
    }
}
