using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EMS.Models
{
    public class Reservation
    {
        public Reservation()
        {
            IsDone = false;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Investor Preferred Topics")]
        public string Topic { get; set; }

        [Required]
        [ForeignKey("Presenter")]
        [Display(Name = "Presenter Name")]
        public int PresenterId { get; set; }
        public Presenter Presenter { get; set; }

        [Required]
        [ForeignKey("Investor")]
        [Display(Name = "Investor Name")]
        public int InvestorId { get; set; }
        public Investor Investor { get; set; }

        [Required]
        [ForeignKey("ConferenceRoom")]
        [Display(Name = "Available Hotel Rooms")]
        public int ConferenceRoomId { get; set; }
        public ConferenceRoom ConferenceRooms { get; set; }

        public DateTime ReservationDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Available Preferred Times")]
        public DateTime StartTime { get; set; }

        public DateTime EndTime
        {
            get { return StartTime.AddHours(1); }
            set { StartTime = value.AddHours(-1); }
        }

        private bool isDone;
        public bool IsDone
        {
            get
            {
                if (DateTime.Now >= EndTime) // Check if current time is greater than or equal to EndTime
                    isDone = true;
                return isDone;
            }
            set { isDone = value; }
        }
    }
}
