using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using System.Globalization;
using EMS.Models;
using EMS.Data;

namespace EMS.Controllers
{
    public class HotelsController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ApplicationDbContext _context;

        public HotelsController(IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            this.hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(List<Hotel> Hotels = null)
        {
            Hotels = Hotels ?? new List<Hotel>();
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            #region Upload CSV
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            #endregion

            var Hotels = GetHotelList(fileName);

            foreach (var Hotel in Hotels)
            {
                _context.Hotel.Add(new Hotel
                {
                    Name = Hotel.Name,
                });
            }

            _context.SaveChanges();

            return Index();
        }

        private List<HotelViewModel> GetHotelList(string fileName)
        {
            List<HotelViewModel> Hotels = new List<HotelViewModel>();
            #region Read CSV
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", fileName);
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var Hotel = csv.GetRecord<HotelViewModel>();
                    Hotels.Add(Hotel);
                }
            }
            #endregion

            #region Create CSV
            path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "FilesTo", "NewFile.csv");
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(Hotels);
            }
            #endregion

            return Hotels;
        }
    }
}
