using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Models
{
    public class ConferenceRoom
    {




        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        [Display(Name ="Hotel Room Number")]
        //   [Index(IsUnique = true)]
        [Remote(action: "VerifyNumber", controller: "ConferenceRoom", ErrorMessage = "The room number must be unique.")]
        public string number { get; set; }

        [Required]
        [ForeignKey("Hotel")]
        [Display(Name = "Hotel Name")]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        [Required]
        public List<RoomTimeSlot> RoomTimeSlots { get; set; }


    }
}
