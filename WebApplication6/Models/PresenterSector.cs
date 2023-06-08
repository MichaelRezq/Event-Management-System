using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication5.Models;

namespace WebApplication1.Models
{
    public class PresenterSector
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Presenter")]
        public int PresenterId { get; set; }
        public Presenter Presenter { get; set; }
        public List<PresenterTimeSlot> PresenterTimeSlots { get; set; }

      
    }
}
