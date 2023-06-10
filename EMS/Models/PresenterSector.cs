using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMS.Models;

namespace EMS.Models
{
    public class PresenterSector
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Sector Name ")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Presenter Name ")]
        [ForeignKey("Presenter")]
        public int PresenterId { get; set; }
        public Presenter Presenter { get; set; }
        public List<PresenterTimeSlot> PresenterTimeSlots { get; set; }

      
    }
}
