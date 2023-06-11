using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using EMS.Models;

namespace EMS.Models
{
    public class InvestorTimeSlot
    {
        [Key]
        public int Id { get; set; }


        private TimeSpan startTime;
        [Required]
        [Display(Name = "Prefarred Start Time")]
       // [Range(typeof(TimeSpan), "00:00", "22:59")]

        [DataType(DataType.Time)]
        public TimeSpan StartTime
        {
            get { return startTime; }
            set
            {
                if (value >= TimeSpan.Zero && value <= TimeSpan.FromDays(1))
                    startTime = value;
                else
                    throw new ArgumentOutOfRangeException("StartTime", "The Start Time must be between 00:00:00 and 23:59:59.");
            }
        }

        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public TimeSpan EndTime
        {
            get { return StartTime.Add(TimeSpan.FromHours(1)); }
            set { StartTime = value.Subtract(TimeSpan.FromHours(1)); }
        }

        public bool Occupied { get; set; }
       
        [Required]
        [Display(Name = "Investors Sectors")]
        [ForeignKey("InvestorSector")]
        public int InvestorSectorId { get; set; }
        public InvestorSector InvestorSectors { get; set; }
    }
}
