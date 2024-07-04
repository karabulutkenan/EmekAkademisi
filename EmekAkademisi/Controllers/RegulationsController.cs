using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmekAkademisi.Data;
using EmekAkademisi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace EmekAkademisi.Controllers
{
    public class RegulationsController : Controller
    {
        private readonly EmekAkademisiContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RegulationsController(EmekAkademisiContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Regulations
        public async Task<IActionResult> Index(string searchString)
        {
            var regulations = from r in _context.Regulations
                              select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                regulations = regulations.Where(s => s.Title.Contains(searchString));
            }

            return View(await regulations.ToListAsync());
        }

        // GET: Regulations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Regulations == null)
            {
                return NotFound();
            }

            var regulation = await _context.Regulations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (regulation == null)
            {
                return NotFound();
            }

            return View(regulation);
        }

        // GET: Regulations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Regulations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] Regulation regulation, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    regulation.FilePath = "/uploads/" + fileName;
                }

                regulation.UploadDate = DateTime.Now;

                _context.Add(regulation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regulation);
        }

        // GET: Regulations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Regulations == null)
            {
                return NotFound();
            }

            var regulation = await _context.Regulations.FindAsync(id);
            if (regulation == null)
            {
                return NotFound();
            }
            return View(regulation);
        }

        // POST: Regulations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,FilePath")] Regulation regulation, IFormFile file)
        {
            if (id != regulation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    regulation.FilePath = "/uploads/" + fileName;
                }

                try
                {
                    _context.Update(regulation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegulationExists(regulation.Id))
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
            return View(regulation);
        }

        // GET: Regulations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Regulations == null)
            {
                return NotFound();
            }

            var regulation = await _context.Regulations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (regulation == null)
            {
                return NotFound();
            }

            return View(regulation);
        }

        // POST: Regulations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Regulations == null)
            {
                return Problem("Entity set 'EmekAkademisiContext.Regulations'  is null.");
            }
            var regulation = await _context.Regulations.FindAsync(id);
            if (regulation != null)
            {
                _context.Regulations.Remove(regulation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegulationExists(int id)
        {
            return (_context.Regulations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
