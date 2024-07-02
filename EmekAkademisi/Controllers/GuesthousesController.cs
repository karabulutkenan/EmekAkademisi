using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmekAkademisi.Data;
using EmekAkademisi.Models;

namespace EmekAkademisi.Controllers
{
    public class GuesthousesController : Controller
    {
        private readonly EmekAkademisiContext _context;

        public GuesthousesController(EmekAkademisiContext context)
        {
            _context = context;
        }

        // GET: Guesthouses
        public async Task<IActionResult> Index(string searchString)
        {
            var guesthouses = from g in _context.Guesthouses
                              select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                guesthouses = guesthouses.Where(s => s.Name.Contains(searchString));
            }

            return View(await guesthouses.ToListAsync());
        }

        // GET: Guesthouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Guesthouses == null)
            {
                return NotFound();
            }

            var guesthouse = await _context.Guesthouses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guesthouse == null)
            {
                return NotFound();
            }

            return View(guesthouse);
        }

        // GET: Guesthouses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guesthouses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Phone")] Guesthouse guesthouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guesthouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guesthouse);
        }

        // GET: Guesthouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Guesthouses == null)
            {
                return NotFound();
            }

            var guesthouse = await _context.Guesthouses.FindAsync(id);
            if (guesthouse == null)
            {
                return NotFound();
            }
            return View(guesthouse);
        }

        // POST: Guesthouses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Phone")] Guesthouse guesthouse)
        {
            if (id != guesthouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guesthouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuesthouseExists(guesthouse.Id))
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
            return View(guesthouse);
        }

        // GET: Guesthouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Guesthouses == null)
            {
                return NotFound();
            }

            var guesthouse = await _context.Guesthouses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guesthouse == null)
            {
                return NotFound();
            }

            return View(guesthouse);
        }

        // POST: Guesthouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Guesthouses == null)
            {
                return Problem("Entity set 'EmekAkademisiContext.Guesthouses'  is null.");
            }
            var guesthouse = await _context.Guesthouses.FindAsync(id);
            if (guesthouse != null)
            {
                _context.Guesthouses.Remove(guesthouse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuesthouseExists(int id)
        {
            return (_context.Guesthouses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
