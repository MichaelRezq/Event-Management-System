using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EMS.Models;
using Microsoft.EntityFrameworkCore;
using EMS.Data;

namespace EMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

   
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Presenter = _context.Presenter.ToArray();
            ViewBag.Investor = _context.Investor.ToArray();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}