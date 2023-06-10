using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Hotel Name")]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<ConferenceRoom> ConferenceRooms { get; set; }

    }
}
