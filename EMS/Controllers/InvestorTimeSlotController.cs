using EMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMS.Models;

namespace EMS.Controllers
{
    public class InvestorTimeSlotController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvestorTimeSlotController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvestorTimeSlot
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InvestorTimeSlot.Include(i => i.InvestorSectors);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvestorTimeSlot/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InvestorTimeSlot == null)
            {
                return NotFound();
            }

            var investorTimeSlot = await _context.InvestorTimeSlot
                .Include(i => i.InvestorSectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (investorTimeSlot == null)
            {
                return NotFound();
            }

            return View(investorTimeSlot);
        }

        // GET: InvestorTimeSlot/Create
        public IActionResult Create()
        {
            ViewData["InvestorSectorId"] = new SelectList(_context.InvestorSector, "Id", "Name");
            return View();
        }

        // POST: InvestorTimeSlot/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,EndTime,InvestorSectorId")] InvestorTimeSlot investorTimeSlot)
        {
            //      if (ModelState.IsValid)
            //    {
            _context.Add(investorTimeSlot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //  }
            //ViewData["InvestorSectorId"] = new SelectList(_context.InvestorSector, "Id", "Name", investorTimeSlot.InvestorSectorId);
            //return View(investorTimeSlot);
        }

        // GET: InvestorTimeSlot/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InvestorTimeSlot == null)
            {
                return NotFound();
            }

            var investorTimeSlot = await _context.InvestorTimeSlot.FindAsync(id);
            if (investorTimeSlot == null)
            {
                return NotFound();
            }
            ViewData["InvestorSectorId"] = new SelectList(_context.InvestorSector, "Id", "Name", investorTimeSlot.InvestorSectorId);
            return View(investorTimeSlot);
        }

        // POST: InvestorTimeSlot/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime,InvestorSectorId")] InvestorTimeSlot investorTimeSlot)
        {
            if (id != investorTimeSlot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(investorTimeSlot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestorTimeSlotExists(investorTimeSlot.Id))
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
            ViewData["InvestorSectorId"] = new SelectList(_context.InvestorSector, "Id", "Name", investorTimeSlot.InvestorSectorId);
            return View(investorTimeSlot);
        }

        // GET: InvestorTimeSlot/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InvestorTimeSlot == null)
            {
                return NotFound();
            }

            var investorTimeSlot = await _context.InvestorTimeSlot
                .Include(i => i.InvestorSectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (investorTimeSlot == null)
            {
                return NotFound();
            }

            return View(investorTimeSlot);
        }

        // POST: InvestorTimeSlot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InvestorTimeSlot == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InvestorTimeSlot'  is null.");
            }
            var investorTimeSlot = await _context.InvestorTimeSlot.FindAsync(id);
            if (investorTimeSlot != null)
            {
                _context.InvestorTimeSlot.Remove(investorTimeSlot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvestorTimeSlotExists(int id)
        {
            return (_context.InvestorTimeSlot?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
