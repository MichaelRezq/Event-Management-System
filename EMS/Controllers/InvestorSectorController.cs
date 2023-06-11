using EMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMS.Models;

namespace EMS.Controllers
{
    public class InvestorSectorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvestorSectorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvestorSector
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InvestorSector.Include(i => i.Investor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvestorSector/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InvestorSector == null)
            {
                return NotFound();
            }

            var investorSector = await _context.InvestorSector
                .Include(i => i.Investor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (investorSector == null)
            {
                return NotFound();
            }

            return View(investorSector);
        }

        // GET: InvestorSector/Create
        public IActionResult Create()
        {
            ViewData["InvestorId"] = new SelectList(_context.Investor, "Id", "Name");
            ViewBag.UserSectors = _context.InvestorSector.ToArray();

            return View();
        }

        // POST: InvestorSector/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,InvestorId")] InvestorSector investorSector)
        {
            //      if (ModelState.IsValid)
            //    {
            _context.Add(investorSector);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Investor");
            //  }
            //ViewData["InvestorId"] = new SelectList(_context.Investor, "Id", "Id", investorSector.InvestorId);
            //return View(investorSector);
        }

        // GET: InvestorSector/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InvestorSector == null)
            {
                return NotFound();
            }

            var investorSector = await _context.InvestorSector.FindAsync(id);
            if (investorSector == null)
            {
                return NotFound();
            }
            ViewData["InvestorId"] = new SelectList(_context.Investor, "Id", "Id", investorSector.InvestorId);
            return View(investorSector);
        }

        // POST: InvestorSector/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,InvestorId")] InvestorSector investorSector)
        {
            if (id != investorSector.Id)
            {
                return NotFound();
            }

 
                try
                {
                    _context.Update(investorSector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestorSectorExists(investorSector.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
 
            }
            ViewData["InvestorId"] = new SelectList(_context.Investor, "Id", "Id", investorSector.InvestorId);
            return RedirectToAction("Index", "Investor");
        }

        // GET: InvestorSector/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InvestorSector == null)
            {
                return NotFound();
            }

            var investorSector = await _context.InvestorSector
                .Include(i => i.Investor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (investorSector == null)
            {
                return NotFound();
            }

            return View(investorSector);
        }

        // POST: InvestorSector/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InvestorSector == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InvestorSector'  is null.");
            }
            var investorSector = await _context.InvestorSector.FindAsync(id);
            if (investorSector != null)
            {
                _context.InvestorSector.Remove(investorSector);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvestorSectorExists(int id)
        {
            return (_context.InvestorSector?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
