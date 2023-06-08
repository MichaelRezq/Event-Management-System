using WebApplication5.Models;

namespace WebApplication1.Models
{
    public class Investor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public List<InvestorSector> InvestorSectors { get; set; }
    }

}
