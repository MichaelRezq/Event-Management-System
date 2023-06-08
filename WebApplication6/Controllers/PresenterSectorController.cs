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
    public class PresenterSectorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PresenterSectorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PresenterSector
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PresenterSector.Include(p => p.Presenter);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PresenterSector/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PresenterSector == null)
            {
                return NotFound();
            }

            var presenterSector = await _context.PresenterSector
                .Include(p => p.Presenter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (presenterSector == null)
            {
                return NotFound();
            }

            return View(presenterSector);
        }

        // GET: PresenterSector/Create
        public IActionResult Create()
        {
            ViewData["PresenterId"] = new SelectList(_context.Presenter, "Id", "Id");
            return View();
        }

        // POST: PresenterSector/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PresenterId")] PresenterSector presenterSector)
        {
       //     if (ModelState.IsValid)
         //   {
                _context.Add(presenterSector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           // }
         //   ViewData["PresenterId"] = new SelectList(_context.Presenter, "Id", "Id", presenterSector.PresenterId);
          //  return View(presenterSector);
        }

        // GET: PresenterSector/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PresenterSector == null)
            {
                return NotFound();
            }

            var presenterSector = await _context.PresenterSector.FindAsync(id);
            if (presenterSector == null)
            {
                return NotFound();
            }
            ViewData["PresenterId"] = new SelectList(_context.Presenter, "Id", "Id", presenterSector.PresenterId);
            return View(presenterSector);
        }

        // POST: PresenterSector/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PresenterId")] PresenterSector presenterSector)
        {
            if (id != presenterSector.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presenterSector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresenterSectorExists(presenterSector.Id))
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
            ViewData["PresenterId"] = new SelectList(_context.Presenter, "Id", "Id", presenterSector.PresenterId);
            return View(presenterSector);
        }

        // GET: PresenterSector/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PresenterSector == null)
            {
                return NotFound();
            }

            var presenterSector = await _context.PresenterSector
                .Include(p => p.Presenter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (presenterSector == null)
            {
                return NotFound();
            }

            return View(presenterSector);
        }

        // POST: PresenterSector/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PresenterSector == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PresenterSector'  is null.");
            }
            var presenterSector = await _context.PresenterSector.FindAsync(id);
            if (presenterSector != null)
            {
                _context.PresenterSector.Remove(presenterSector);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresenterSectorExists(int id)
        {
          return (_context.PresenterSector?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
