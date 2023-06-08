using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace WebApplication5.Models
{
    public class InvestorSector
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Investor")]
        public int InvestorId { get; set; }
        public Investor Investor { get; set; }


       
        public List<InvestorTimeSlot> PreferredTimeSlots { get; set; }
    }
}
