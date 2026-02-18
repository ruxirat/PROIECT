using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ratiu_Ruxandra_Proiect.Data;
using Ratiu_Ruxandra_Proiect.Models;

namespace Ratiu_Ruxandra_Proiect.Controllers
{
    public class ServicesController : Controller
    {
        private readonly Ratiu_Ruxandra_ProiectContext _context;

        public ServicesController(Ratiu_Ruxandra_ProiectContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var services = _context.Service.Include(s => s.Specialization);
            return View(await services.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var service = await _context.Service
                .Include(s => s.Specialization)
                .FirstOrDefaultAsync(m => m.ServiceId == id);

            if (service == null) return NotFound();

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            ViewData["SpecializationId"] =
                new SelectList(_context.Specialization, "SpecializationId", "Name");

            return View();
        }

        // POST: Services/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,Name,DurationMinutes,Price,SpecializationId")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SpecializationId"] =
                new SelectList(_context.Specialization, "SpecializationId", "Name", service.SpecializationId);

            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var service = await _context.Service.FindAsync(id);
            if (service == null) return NotFound();

            ViewData["SpecializationId"] =
                new SelectList(_context.Specialization, "SpecializationId", "Name", service.SpecializationId);

            return View(service);
        }

        // POST: Services/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,Name,DurationMinutes,Price,SpecializationId")] Service service)
        {
            if (id != service.ServiceId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.ServiceId)) return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["SpecializationId"] =
                new SelectList(_context.Specialization, "SpecializationId", "Name", service.SpecializationId);

            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var service = await _context.Service
                .Include(s => s.Specialization)
                .FirstOrDefaultAsync(m => m.ServiceId == id);

            if (service == null) return NotFound();

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Service.FindAsync(id);
            if (service != null)
            {
                _context.Service.Remove(service);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Service.Any(e => e.ServiceId == id);
        }
    }
}
