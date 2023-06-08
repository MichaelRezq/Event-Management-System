using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication5.Models;
using WebApplication6.Data;

namespace WebApplication6.Controllers
{
    public class PresenterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PresenterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Presenter
        public async Task<IActionResult> Index()
        {
              return _context.Presenter != null ? 
                          View(await _context.Presenter.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Presenter'  is null.");
        }

        // GET: Presenter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Presenter == null)
            {
                return NotFound();
            }

            var presenter = await _context.Presenter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (presenter == null)
            {
                return NotFound();
            }
            var PresenterSectors = await _context.PresenterSector.Where(m => m.PresenterId == id).ToListAsync();
            var PresenterSectorsTimeSlot = await _context.PresenterTimeSlot.ToListAsync();

            var tuple = new Tuple<Presenter, List<PresenterSector>, List<PresenterTimeSlot>>(presenter, PresenterSectors, PresenterSectorsTimeSlot);

            return View(tuple);

        }

        // GET: Presenter/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Presenter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Mobile")] Presenter presenter)
        {
        //    if (ModelState.IsValid)
          //  {
                _context.Add(presenter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(presenter);
        }

        // GET: Presenter/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Presenter == null)
            {
                return NotFound();
            }

            var presenter = await _context.Presenter.FindAsync(id);
            if (presenter == null)
            {
                return NotFound();
            }
            return View(presenter);
        }

        // POST: Presenter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Mobile")] Presenter presenter)
        {
            if (id != presenter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presenter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresenterExists(presenter.Id))
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
            return View(presenter);
        }

        // GET: Presenter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Presenter == null)
            {
                return NotFound();
            }

            var presenter = await _context.Presenter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (presenter == null)
            {
                return NotFound();
            }

            return View(presenter);
        }

        // POST: Presenter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Presenter == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Presenter'  is null.");
            }
            var presenter = await _context.Presenter.FindAsync(id);
            if (presenter != null)
            {
                _context.Presenter.Remove(presenter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresenterExists(int id)
        {
          return (_context.Presenter?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
