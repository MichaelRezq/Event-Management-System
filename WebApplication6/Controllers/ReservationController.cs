using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication6.Data;

namespace WebApplication6.Controllers
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
            ViewData["Topic"] = new SelectList(_context.PresenterSector, "Name", "Name");
            ViewData["ConferenceRoomId"] = new SelectList(_context.ConferenceRoom, "Id", "number");
            ViewData["InvestorId"] = new SelectList(_context.Investor, "Name", "Name");
            ViewData["PresenterId"] = new SelectList(_context.Presenter, "Name", "Name");
            return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Topic,PresenterId,InvestorId,ConferenceRoomId,ReservationDate,StartTime,EndTime")] Reservation reservation)
        {
            //if (ModelState.IsValid)
          //  {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
         //   }
          //  ViewData["ConferenceRoomId"] = new SelectList(_context.ConferenceRoom, "Id", "number", reservation.ConferenceRoomId);
          //  ViewData["InvestorId"] = new SelectList(_context.Investor, "Id", "Id", reservation.InvestorId);
          //  ViewData["PresenterId"] = new SelectList(_context.Presenter, "Id", "Id", reservation.PresenterId);
           // return View(reservation);
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
            ViewData["ConferenceRoomId"] = new SelectList(_context.ConferenceRoom, "Id", "number", reservation.ConferenceRoomId);
            ViewData["InvestorId"] = new SelectList(_context.Investor, "Id", "Id", reservation.InvestorId);
            ViewData["PresenterId"] = new SelectList(_context.Presenter, "Id", "Id", reservation.PresenterId);
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
