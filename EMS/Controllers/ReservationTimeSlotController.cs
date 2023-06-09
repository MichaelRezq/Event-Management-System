using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMS.Models;

namespace EMS.Controllers
{
    public class ReservationTimeSlotController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationTimeSlotController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReservationTimeSlot
        public async Task<IActionResult> Index()
        {
            return _context.ReservationTimeSlot != null ?
                        View(await _context.ReservationTimeSlot.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.ReservationTimeSlot'  is null.");
        }

        // GET: ReservationTimeSlot/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReservationTimeSlot == null)
            {
                return NotFound();
            }

            var reservationTimeSlot = await _context.ReservationTimeSlot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservationTimeSlot == null)
            {
                return NotFound();
            }

            return View(reservationTimeSlot);
        }

        // GET: ReservationTimeSlot/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReservationTimeSlot/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,EndTime")] ReservationTimeSlot reservationTimeSlot)
        {
            //  if (ModelState.IsValid)
            //    {
            _context.Add(reservationTimeSlot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //  }
            // return View(reservationTimeSlot);
        }

        // GET: ReservationTimeSlot/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReservationTimeSlot == null)
            {
                return NotFound();
            }

            var reservationTimeSlot = await _context.ReservationTimeSlot.FindAsync(id);
            if (reservationTimeSlot == null)
            {
                return NotFound();
            }
            return View(reservationTimeSlot);
        }

        // POST: ReservationTimeSlot/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime")] ReservationTimeSlot reservationTimeSlot)
        {
            if (id != reservationTimeSlot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationTimeSlot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationTimeSlotExists(reservationTimeSlot.Id))
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
            return View(reservationTimeSlot);
        }

        // GET: ReservationTimeSlot/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReservationTimeSlot == null)
            {
                return NotFound();
            }

            var reservationTimeSlot = await _context.ReservationTimeSlot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservationTimeSlot == null)
            {
                return NotFound();
            }

            return View(reservationTimeSlot);
        }

        // POST: ReservationTimeSlot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReservationTimeSlot == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ReservationTimeSlot'  is null.");
            }
            var reservationTimeSlot = await _context.ReservationTimeSlot.FindAsync(id);
            if (reservationTimeSlot != null)
            {
                _context.ReservationTimeSlot.Remove(reservationTimeSlot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationTimeSlotExists(int id)
        {
            return (_context.ReservationTimeSlot?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
