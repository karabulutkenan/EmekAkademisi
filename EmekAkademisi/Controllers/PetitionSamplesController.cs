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
    public class PetitionSamplesController : Controller
    {
        private readonly EmekAkademisiContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PetitionSamplesController(EmekAkademisiContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: PetitionSamples
        public async Task<IActionResult> Index(string searchString)
        {
            var petitionSamples = from ps in _context.PetitionSamples
                                  select ps;

            if (!String.IsNullOrEmpty(searchString))
            {
                petitionSamples = petitionSamples.Where(s => s.Title.Contains(searchString));
            }

            return View(await petitionSamples.ToListAsync());
        }

        // GET: PetitionSamples/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PetitionSamples == null)
            {
                return NotFound();
            }

            var petitionSample = await _context.PetitionSamples
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petitionSample == null)
            {
                return NotFound();
            }

            return View(petitionSample);
        }

        // GET: PetitionSamples/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetitionSamples/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] PetitionSample petitionSample, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolderPath))
                    {
                        Directory.CreateDirectory(uploadsFolderPath);
                    }
                    var filePath = Path.Combine(uploadsFolderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    petitionSample.FilePath = "/uploads/" + fileName;
                }

                petitionSample.UploadDate = DateTime.Now;

                _context.Add(petitionSample);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petitionSample);
        }

        // GET: PetitionSamples/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PetitionSamples == null)
            {
                return NotFound();
            }

            var petitionSample = await _context.PetitionSamples.FindAsync(id);
            if (petitionSample == null)
            {
                return NotFound();
            }
            return View(petitionSample);
        }

        // POST: PetitionSamples/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,FilePath")] PetitionSample petitionSample, IFormFile file)
        {
            if (id != petitionSample.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolderPath))
                    {
                        Directory.CreateDirectory(uploadsFolderPath);
                    }
                    var filePath = Path.Combine(uploadsFolderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    petitionSample.FilePath = "/uploads/" + fileName;
                }
                else
                {
                    // Dosya yüklenmediğinde mevcut dosya yolunu koruyun
                    _context.Entry(petitionSample).Property(x => x.FilePath).IsModified = false;
                }

                try
                {
                    _context.Update(petitionSample);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetitionSampleExists(petitionSample.Id))
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
            return View(petitionSample);
        }

        // GET: PetitionSamples/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PetitionSamples == null)
            {
                return NotFound();
            }

            var petitionSample = await _context.PetitionSamples
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petitionSample == null)
            {
                return NotFound();
            }

            return View(petitionSample);
        }

        // POST: PetitionSamples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PetitionSamples == null)
            {
                return Problem("Entity set 'EmekAkademisiContext.PetitionSamples'  is null.");
            }
            var petitionSample = await _context.PetitionSamples.FindAsync(id);
            if (petitionSample != null)
            {
                _context.PetitionSamples.Remove(petitionSample);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetitionSampleExists(int id)
        {
            return (_context.PetitionSamples?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
