using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using EMS.Models;

namespace EMS.Models
{
    public class Investor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Investor Name")]
        [DataType(DataType.Text)]
        [MaxLength(50)]

        public string Name { get; set; }
        [Required]
        [Display(Name = "Mobile")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
        public List<InvestorSector> InvestorSectors { get; set; }
    }

}
