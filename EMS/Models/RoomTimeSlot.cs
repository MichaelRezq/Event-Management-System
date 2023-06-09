﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Models
{
    public class RoomTimeSlot
    {
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

        public bool Occupied { get; set; }


        [ForeignKey("ConferenceRoom")]
        public int ConferenceRoomId { get; set; }
        public ConferenceRoom ConferenceRoom { get; set; }
    }
}
