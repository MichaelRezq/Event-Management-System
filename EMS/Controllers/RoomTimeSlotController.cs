using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Data;
using EMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EMS.Controllers
{
    public class RoomTimeSlotController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomTimeSlotController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoomTimeSlot
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RoomTimeSlot.Include(r => r.ConferenceRoom);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RoomTimeSlot/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RoomTimeSlot == null)
            {
                return NotFound();
            }

            var roomTimeSlot = await _context.RoomTimeSlot
                .Include(r => r.ConferenceRoom)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomTimeSlot == null)
            {
                return NotFound();
            }

            return View(roomTimeSlot);
        }

        // GET: RoomTimeSlot/Create
        public IActionResult Create()
        {
            ViewData["ConferenceRoomId"] = new SelectList(_context.ConferenceRoom, "Id", "number");
            ViewBag.RoomTimeSlots = _context.RoomTimeSlot.ToArray();

            return View();
        }

        // POST: RoomTimeSlot/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,EndTime,ConferenceRoomId")] RoomTimeSlot roomTimeSlot)
        {
            //  if (ModelState.IsValid)
            //  {
            _context.Add(roomTimeSlot);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Hotel");
            //  }
            //  ViewData["ConferenceRoomId"] = new SelectList(_context.ConferenceRoom, "Id", "number", roomTimeSlot.ConferenceRoomId);
            //  return View(roomTimeSlot);
        }

        // GET: RoomTimeSlot/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RoomTimeSlot == null)
            {
                return NotFound();
            }

            var roomTimeSlot = await _context.RoomTimeSlot.FindAsync(id);
            if (roomTimeSlot == null)
            {
                return NotFound();
            }
            ViewData["ConferenceRoomId"] = new SelectList(_context.ConferenceRoom, "Id", "number", roomTimeSlot.ConferenceRoomId);
            return View(roomTimeSlot);
        }

        // POST: RoomTimeSlot/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime,ConferenceRoomId")] RoomTimeSlot roomTimeSlot)
        {
            if (id != roomTimeSlot.Id)
            {
                return NotFound();
            }

    
                try
                {
                    _context.Update(roomTimeSlot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomTimeSlotExists(roomTimeSlot.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                
            }
            ViewData["ConferenceRoomId"] = new SelectList(_context.ConferenceRoom, "Id", "number", roomTimeSlot.ConferenceRoomId);
            return RedirectToAction("Index", "Hotel");
        }

        // GET: RoomTimeSlot/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RoomTimeSlot == null)
            {
                return NotFound();
            }

            var roomTimeSlot = await _context.RoomTimeSlot
                .Include(r => r.ConferenceRoom)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomTimeSlot == null)
            {
                return NotFound();
            }

            return View(roomTimeSlot);
        }

        // POST: RoomTimeSlot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RoomTimeSlot == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RoomTimeSlot'  is null.");
            }
            var roomTimeSlot = await _context.RoomTimeSlot.FindAsync(id);
            if (roomTimeSlot != null)
            {
                _context.RoomTimeSlot.Remove(roomTimeSlot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomTimeSlotExists(int id)
        {
            return (_context.RoomTimeSlot?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
