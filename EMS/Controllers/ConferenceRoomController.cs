using EMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMS.Models;

namespace EMS.Controllers
{
    public class ConferenceRoomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConferenceRoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConferenceRoom
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ConferenceRoom.Include(c => c.Hotel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ConferenceRoom/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ConferenceRoom == null)
            {
                return NotFound();
            }

            var conferenceRoom = await _context.ConferenceRoom
                .Include(c => c.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conferenceRoom == null)
            {
                return NotFound();
            }

            return View(conferenceRoom);
        }

        // GET: ConferenceRoom/Create
        public IActionResult Create()
        {
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Name");
            ViewBag.RoomNumber = _context.ConferenceRoom.ToArray();
            return View();
        }

        public async Task<IActionResult> OnGetVerifyNumber(string number)
        {
            var room = await _context.ConferenceRoom.FirstOrDefaultAsync(r => r.number == number);

            if (room != null)
            {
                return new JsonResult(false);
            }

            return new JsonResult(true);
        }

        // POST: ConferenceRoom/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,number,HotelId")] ConferenceRoom conferenceRoom)
        {
            //      if (ModelState.IsValid)
            //    {
            _context.Add(conferenceRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //    }
            //     ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Name", conferenceRoom.HotelId);
            //   return View(conferenceRoom);
        }

        // GET: ConferenceRoom/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ConferenceRoom == null)
            {
                return NotFound();
            }

            var conferenceRoom = await _context.ConferenceRoom.FindAsync(id);
            if (conferenceRoom == null)
            {
                return NotFound();
            }
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Name", conferenceRoom.HotelId);
            return View(conferenceRoom);
        }

        // POST: ConferenceRoom/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,number,HotelId")] ConferenceRoom conferenceRoom)
        {
            if (id != conferenceRoom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conferenceRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConferenceRoomExists(conferenceRoom.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HotelId"] = new SelectList(_context.Hotel, "Id", "Name", conferenceRoom.HotelId);
            return View(conferenceRoom);
        }

        // GET: ConferenceRoom/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ConferenceRoom == null)
            {
                return NotFound();
            }

            var conferenceRoom = await _context.ConferenceRoom
                .Include(c => c.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conferenceRoom == null)
            {
                return NotFound();
            }

            return View(conferenceRoom);
        }

        // POST: ConferenceRoom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ConferenceRoom == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ConferenceRoom'  is null.");
            }
            var conferenceRoom = await _context.ConferenceRoom.FindAsync(id);
            if (conferenceRoom != null)
            {
                _context.ConferenceRoom.Remove(conferenceRoom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConferenceRoomExists(int id)
        {
            return (_context.ConferenceRoom?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
