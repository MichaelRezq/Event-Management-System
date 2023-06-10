using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMS.Models;

namespace EMS.Models
{
    public class InvestorSector
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Sector Name")]
        [DataType(DataType.Text)]

        public string Name { get; set; }

        [Required]
        [Display(Name = "Investor Name")]
        [DataType(DataType.Text)]

        [ForeignKey("Investor")]
        public int InvestorId { get; set; }
        public Investor Investor { get; set; }


       
        public List<InvestorTimeSlot> PreferredTimeSlots { get; set; }
    }
}
