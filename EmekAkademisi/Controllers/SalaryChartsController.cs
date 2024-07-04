using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmekAkademisi.Data;
using EmekAkademisi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EmekAkademisi.Controllers
{
    public class SalaryChartsController : Controller
    {
        private readonly EmekAkademisiContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SalaryChartsController(EmekAkademisiContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: SalaryCharts
        public async Task<IActionResult> Index(string? searchString)
        {
            if (_context.SalaryCharts == null)
            {
                return Problem("Entity set 'EmekAkademisiContext.SalaryCharts' is null.");
            }

            var salaryCharts = from sc in _context.SalaryCharts
                               select sc;

            if (!string.IsNullOrEmpty(searchString))
            {
                salaryCharts = salaryCharts.Where(s => s.Title.Contains(searchString));
            }

            return View(await salaryCharts.ToListAsync());
        }

        // GET: SalaryCharts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SalaryCharts == null)
            {
                return NotFound();
            }

            var salaryChart = await _context.SalaryCharts
                .FirstOrDefaultAsync(m => m.Id == id);

            if (salaryChart == null)
            {
                return NotFound();
            }

            return View(salaryChart);
        }

        // GET: SalaryCharts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalaryCharts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] SalaryChart salaryChart, IFormFile? file)
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

                    salaryChart.ImagePath = "/uploads/" + fileName;
                }

                salaryChart.UploadDate = DateTime.Now;

                _context.Add(salaryChart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salaryChart);
        }

        // GET: SalaryCharts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SalaryCharts == null)
            {
                return NotFound();
            }

            var salaryChart = await _context.SalaryCharts.FindAsync(id);
            if (salaryChart == null)
            {
                return NotFound();
            }
            return View(salaryChart);
        }

        // POST: SalaryCharts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ImagePath")] SalaryChart salaryChart, IFormFile? file)
        {
            if (id != salaryChart.Id)
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

                    salaryChart.ImagePath = "/uploads/" + fileName;
                }
                else
                {
                    _context.Entry(salaryChart).Property(x => x.ImagePath).IsModified = false;
                }

                salaryChart.UploadDate = DateTime.Now;

                try
                {
                    _context.Update(salaryChart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaryChartExists(salaryChart.Id))
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
            return View(salaryChart);
        }

        // GET: SalaryCharts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SalaryCharts == null)
            {
                return NotFound();
            }

            var salaryChart = await _context.SalaryCharts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salaryChart == null)
            {
                return NotFound();
            }

            return View(salaryChart);
        }

        // POST: SalaryCharts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SalaryCharts == null)
            {
                return Problem("Entity set 'EmekAkademisiContext.SalaryCharts' is null.");
            }

            var salaryChart = await _context.SalaryCharts.FindAsync(id);
            if (salaryChart != null)
            {
                _context.SalaryCharts.Remove(salaryChart);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SalaryChartExists(int id)
        {
            return (_context.SalaryCharts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
