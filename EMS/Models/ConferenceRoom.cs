using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Models
{
    public class ConferenceRoom
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string number { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        [Required]
        public List<RoomTimeSlot> RoomTimeSlots { get; set; }


    }
}
