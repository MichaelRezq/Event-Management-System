using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<ConferenceRoom> ConferenceRooms { get; set; }

    }
}
