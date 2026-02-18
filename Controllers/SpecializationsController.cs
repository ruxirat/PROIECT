using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ratiu_Ruxandra_Proiect.Data;
using Ratiu_Ruxandra_Proiect.Models;

namespace Ratiu_Ruxandra_Proiect.Controllers
{
    public class SpecializationsController : Controller
    {
        private readonly Ratiu_Ruxandra_ProiectContext _context;

        public SpecializationsController(Ratiu_Ruxandra_ProiectContext context)
        {
            _context = context;
        }

        // GET: Specializations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Specialization.ToListAsync());
        }

        // GET: Specializations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialization = await _context.Specialization
                .FirstOrDefaultAsync(m => m.SpecializationId == id);
            if (specialization == null)
            {
                return NotFound();
            }

            return View(specialization);
        }

        // GET: Specializations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Specializations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpecializationId,Name")] Specialization specialization)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialization);
        }

        // GET: Specializations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialization = await _context.Specialization.FindAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }
            return View(specialization);
        }

        // POST: Specializations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpecializationId,Name")] Specialization specialization)
        {
            if (id != specialization.SpecializationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecializationExists(specialization.SpecializationId))
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
            return View(specialization);
        }

        // GET: Specializations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialization = await _context.Specialization
                .FirstOrDefaultAsync(m => m.SpecializationId == id);
            if (specialization == null)
            {
                return NotFound();
            }

            return View(specialization);
        }

        // POST: Specializations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialization = await _context.Specialization.FindAsync(id);
            if (specialization != null)
            {
                _context.Specialization.Remove(specialization);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecializationExists(int id)
        {
            return _context.Specialization.Any(e => e.SpecializationId == id);
        }
    }
}
