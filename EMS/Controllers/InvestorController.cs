using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMS.Models;
using EMS.Models;

namespace EMS.Controllers
{
    public class InvestorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvestorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Investor
        public async Task<IActionResult> Index()
        {
            return _context.Investor != null ?
                        View(await _context.Investor.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Investor'  is null.");
        }

        // GET: Investor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Investor == null)
            {
                return NotFound();
            }

            var investor = await _context.Investor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (investor == null)
            {
                return NotFound();
            }
            var investorSectors = await _context.InvestorSector.Where(m => m.InvestorId == id).ToListAsync();
            var investorSectorsTimeSlot = await _context.InvestorTimeSlot.ToListAsync();

            var tuple = new Tuple<Investor, List<InvestorSector>, List<InvestorTimeSlot>>(investor, investorSectors, investorSectorsTimeSlot);

            return View(tuple);

        }

        // GET: Investor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Investor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Mobile")] Investor investor)
        {
            //      if (ModelState.IsValid)
            //    {
            _context.Add(investor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //    }
            //  return View(investor);
        }

        // GET: Investor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Investor == null)
            {
                return NotFound();
            }

            var investor = await _context.Investor.FirstOrDefaultAsync(m => m.Id == id);
            if (investor == null)
            {
                return NotFound();
            }

            var investorSectors = await _context.InvestorSector.Where(m => m.InvestorId == id).ToListAsync();
            var investorTimeSlots = await _context.InvestorTimeSlot.ToListAsync();

            var model = new Tuple<Investor, List<InvestorSector>, List<InvestorTimeSlot>>(investor, investorSectors, investorTimeSlots);

            return View(model);

        }

        // POST: Investor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Mobile")] Investor investor)
        {
            if (id != investor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(investor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestorExists(investor.Id))
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
            return View(investor);
        }

        // GET: Investor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Investor == null)
            {
                return NotFound();
            }

            var investor = await _context.Investor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (investor == null)
            {
                return NotFound();
            }

            return View(investor);
        }

        // POST: Investor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Investor == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Investor'  is null.");
            }
            var investor = await _context.Investor.FindAsync(id);
            if (investor != null)
            {
                _context.Investor.Remove(investor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvestorExists(int id)
        {
            return (_context.Investor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
