using EMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMS.Models;

namespace EMS.Controllers
{
    public class PresenterTimeSlotController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PresenterTimeSlotController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PresenterTimeSlot
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PresenterTimeSlot.Include(p => p.PresenterSectors);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PresenterTimeSlot/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PresenterTimeSlot == null)
            {
                return NotFound();
            }

            var presenterTimeSlot = await _context.PresenterTimeSlot
                .Include(p => p.PresenterSectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (presenterTimeSlot == null)
            {
                return NotFound();
            }

            return View(presenterTimeSlot);
        }

        // GET: PresenterTimeSlot/Create
        public IActionResult Create()
        {
            ViewData["PresenterSectorId"] = new SelectList(_context.PresenterSector, "Id", "Name");
            ViewBag.TimeSlots = _context.PresenterTimeSlot.ToArray();

            return View();
        }

        // POST: PresenterTimeSlot/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,EndTime,PresenterSectorId")] PresenterTimeSlot presenterTimeSlot)
        {
            //      if (ModelState.IsValid)
            //    {
            _context.Add(presenterTimeSlot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //  ViewData["PresenterSectorId"] = new SelectList(_context.PresenterSector, "Id", "Name", presenterTimeSlot.PresenterSectorId);
            //return View(presenterTimeSlot);
        }

        // GET: PresenterTimeSlot/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PresenterTimeSlot == null)
            {
                return NotFound();
            }

            var presenterTimeSlot = await _context.PresenterTimeSlot.FindAsync(id);
            if (presenterTimeSlot == null)
            {
                return NotFound();
            }
            ViewData["PresenterSectorId"] = new SelectList(_context.PresenterSector, "Id", "Name", presenterTimeSlot.PresenterSectorId);
            return View(presenterTimeSlot);
        }

        // POST: PresenterTimeSlot/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime,PresenterSectorId")] PresenterTimeSlot presenterTimeSlot)
        {
            if (id != presenterTimeSlot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presenterTimeSlot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresenterTimeSlotExists(presenterTimeSlot.Id))
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
            ViewData["PresenterSectorId"] = new SelectList(_context.PresenterSector, "Id", "Name", presenterTimeSlot.PresenterSectorId);
            return View(presenterTimeSlot);
        }

        // GET: PresenterTimeSlot/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PresenterTimeSlot == null)
            {
                return NotFound();
            }

            var presenterTimeSlot = await _context.PresenterTimeSlot
                .Include(p => p.PresenterSectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (presenterTimeSlot == null)
            {
                return NotFound();
            }

            return View(presenterTimeSlot);
        }

        // POST: PresenterTimeSlot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PresenterTimeSlot == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PresenterTimeSlot'  is null.");
            }
            var presenterTimeSlot = await _context.PresenterTimeSlot.FindAsync(id);
            if (presenterTimeSlot != null)
            {
                _context.PresenterTimeSlot.Remove(presenterTimeSlot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresenterTimeSlotExists(int id)
        {
            return (_context.PresenterTimeSlot?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
