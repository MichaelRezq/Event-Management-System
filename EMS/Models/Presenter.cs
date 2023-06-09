using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class Presenter
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Presenter Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [Display(Name ="Mobile")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
        public List<PresenterSector> PresenterSectors { get; set; }
    }

}
