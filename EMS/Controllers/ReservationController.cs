using EMS.Data;
using EMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EMS.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservation
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservation.Include(r => r.ConferenceRooms).Include(r => r.Investor).Include(r => r.Presenter);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.ConferenceRooms)
                .Include(r => r.Investor)
                .Include(r => r.Presenter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservation/Create
        public IActionResult Create()
        {


            ViewData["Topic"] = new SelectList(_context.InvestorSector, "Id", "Name", "Select Topic");
            ViewBag.Topics = _context.InvestorSector.ToArray();
            ViewData["TimeSlots"] = new SelectList(_context.InvestorTimeSlot, "Id", "StartTime");
            ViewData["ConferenceRoomId"] = new SelectList(_context.ConferenceRoom, "Id", "number");
            ViewData["InvestorId"] = new SelectList(_context.Investor, "Id", "Name","Select Investor");
            ViewData["PresenterId"] = new SelectList(_context.Presenter, "Id", "Name");


            return View();
        }



        [HttpPost]
        public List<Presenter> GetPresenters(string topicname, string timeslot)
        {
            TimeSpan selectedTimeslot;
            if (!TimeSpan.TryParse(timeslot, out selectedTimeslot))
            {
                // Handle invalid timeslot input
                return new List<Presenter>(); // or return an appropriate response
            }

            var presenters = _context.Presenter
                .Where(x => x.PresenterSectors.Any(s => s.Name == topicname &&
                            s.PresenterTimeSlots.Any(t => t.StartTime == selectedTimeslot && t.Occupied == false)))
                .Select(x => new Presenter
                {
                    Id = x.Id,
                    Name = x.Name,
                    PresenterSectors = x.PresenterSectors
                })
                .ToList();

            return presenters;
        }

        [HttpPost]
        public List<InvestorSector> GetInvestorSectors(int id)
        {
            var investorSectors = _context.InvestorSector.Where(x => x.InvestorId == id).Select(x => new InvestorSector
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return investorSectors;
        }

        [HttpPost]
        public List<InvestorTimeSlot> GetInvestorTimeSlots(int id)
        {
            var investorTimeSlot = _context.InvestorTimeSlot.Where(x => x.InvestorSectorId == id && x.Occupied == false).Select(x => new InvestorTimeSlot
            {
                Id = x.Id,
                StartTime = x.StartTime
            }).ToList();

            return investorTimeSlot;
        }


        public List<ConferenceRoom> GetConferenceRooms(string starttime)
        {
            TimeSpan selectedStartTime;
            if (!TimeSpan.TryParse(starttime, out selectedStartTime))
            {
                // Handle invalid starttime input
                return new List<ConferenceRoom>(); // or return an appropriate response
            }

            var conferenceRooms = _context.ConferenceRoom
                .Where(x => x.RoomTimeSlots.Any(t => t.StartTime == selectedStartTime && t.Occupied == false))
                .Select(x => new ConferenceRoom
                {
                    Id = x.Id,
                    number = x.number,
                    HotelId = x.HotelId,
                    Hotel = x.Hotel
                })
                .ToList();

            return conferenceRooms;
        }





        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Topic,PresenterId,InvestorId,ConferenceRoomId,ReservationDate,StartTime,EndTime")] Reservation reservation)
        {
            // Updating the investor occupied
            var investor = await _context.Investor.FindAsync(reservation.InvestorId);
            var sectorIdForInvestor = await _context.InvestorSector
                .Where(x => x.InvestorId == reservation.InvestorId && x.Name == reservation.Topic)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();


            var investorTimeSlot = await _context.InvestorTimeSlot
                .FirstOrDefaultAsync(y => y.InvestorSectorId == sectorIdForInvestor && y.StartTime == reservation.StartTime.TimeOfDay);


            if (investorTimeSlot != null)
            {
                investorTimeSlot.Occupied = true;
                _context.Entry(investorTimeSlot).State = EntityState.Modified;
            }

            // updating the presenter occupied
            var presenter = await _context.Presenter.FindAsync(reservation.PresenterId);
            var sectorIdForPresenter = await _context.PresenterSector
                .Where(x => x.PresenterId == reservation.PresenterId && x.Name == reservation.Topic)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();


            var presenterTimeSlot = await _context.PresenterTimeSlot
                .FirstOrDefaultAsync(y => y.PresenterSectorId == sectorIdForPresenter && y.StartTime == reservation.StartTime.TimeOfDay);


            if (presenterTimeSlot != null)
            {
                presenterTimeSlot.Occupied = true;
                _context.Entry(presenterTimeSlot).State = EntityState.Modified;
            }


            // updating the Room occupied
            var roomTime = await _context.RoomTimeSlot
                .FirstOrDefaultAsync(y => y.ConferenceRoomId == reservation.ConferenceRoomId && y.StartTime == reservation.StartTime.TimeOfDay);


            if (roomTime != null)
            {
                roomTime.Occupied = true;
                _context.Entry(roomTime).State = EntityState.Modified;
            }


            _context.Add(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Reservation");
            
        }

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {


            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["Topic"] = new SelectList(_context.InvestorSector, "Id", "Name", "Select Topic");
            ViewBag.Topics = _context.InvestorSector.ToArray();
            ViewData["TimeSlots"] = new SelectList(_context.InvestorTimeSlot, "Id", "StartTime");
            ViewData["ConferenceRoomId"] = new SelectList(_context.ConferenceRoom, "Id", "number");
            ViewData["InvestorId"] = new SelectList(_context.Investor, "Id", "Name", "Select Investor");
            ViewData["PresenterId"] = new SelectList(_context.Presenter, "Id", "Name");
            return View(reservation);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Topic,PresenterId,InvestorId,ConferenceRoomId,ReservationDate,StartTime,EndTime")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["ConferenceRoomId"] = new SelectList(_context.ConferenceRoom, "Id", "number", reservation.ConferenceRoomId);
            ViewData["InvestorId"] = new SelectList(_context.Investor, "Id", "Id", reservation.InvestorId);
            ViewData["PresenterId"] = new SelectList(_context.Presenter, "Id", "Id", reservation.PresenterId);
            return View(reservation);
        }

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.ConferenceRooms)
                .Include(r => r.Investor)
                .Include(r => r.Presenter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reservation'  is null.");
            }
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservation.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return (_context.Reservation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
