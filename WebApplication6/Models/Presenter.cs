namespace WebApplication1.Models
{
    public class Presenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public List<PresenterSector> PresenterSectors { get; set; }
    }

}
