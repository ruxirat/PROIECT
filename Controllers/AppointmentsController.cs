using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ratiu_Ruxandra_Proiect.Data;
using Ratiu_Ruxandra_Proiect.Models;

namespace Ratiu_Ruxandra_Proiect.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly Ratiu_Ruxandra_ProiectContext _context;

        public AppointmentsController(Ratiu_Ruxandra_ProiectContext context)
        {
            _context = context;
        }

        // Helper: dropdown doctors "LastName (Specialization)"
        private SelectList BuildDoctorsSelectList(int? selectedDoctorId = null)
        {
            var doctors = _context.Doctor
                .Include(d => d.Specialization)
                .AsNoTracking()
                .Select(d => new
                {
                    d.DoctorId,
                    Display = d.LastName + " (" + (d.Specialization != null ? d.Specialization.Name : "N/A") + ")"
                })
                .ToList();

            return new SelectList(doctors, "DoctorId", "Display", selectedDoctorId);
        }

        // Helper: dropdown services "Name (Specialization)"
        private SelectList BuildServicesSelectList(int? selectedServiceId = null)
        {
            var services = _context.Service
                .Include(s => s.Specialization)
                .AsNoTracking()
                .Select(s => new
                {
                    s.ServiceId,
                    Display = s.Name + " (" + (s.Specialization != null ? s.Specialization.Name : "N/A") + ")"
                })
                .ToList();

            return new SelectList(services, "ServiceId", "Display", selectedServiceId);
        }

        // GET: Appointments (Lab3)
        public async Task<IActionResult> Index(string? sortOrder, string? searchString)
        {
            ViewData["CurrentSort"] = sortOrder;

            ViewData["DateSortParm"] = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["PatientSortParm"] = sortOrder == "patient" ? "patient_desc" : "patient";
            ViewData["DoctorSortParm"] = sortOrder == "doctor" ? "doctor_desc" : "doctor";

            ViewData["CurrentFilter"] = searchString;

            var appointments = _context.Appointment
                .Include(a => a.Doctor).ThenInclude(d => d.Specialization)
                .Include(a => a.Patient)
                .Include(a => a.Service).ThenInclude(s => s.Specialization)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var s = searchString.Trim();
                appointments = appointments.Where(a =>
                    (a.Patient != null && a.Patient.FullName.Contains(s)) ||
                    (a.Doctor != null && a.Doctor.LastName.Contains(s)) ||
                    (a.Service != null && a.Service.Name.Contains(s))
                );
            }

            appointments = sortOrder switch
            {
                "date_desc" => appointments.OrderByDescending(a => a.DateTime),
                "patient" => appointments.OrderBy(a => a.Patient!.FullName),
                "patient_desc" => appointments.OrderByDescending(a => a.Patient!.FullName),
                "doctor" => appointments.OrderBy(a => a.Doctor!.LastName),
                "doctor_desc" => appointments.OrderByDescending(a => a.Doctor!.LastName),
                _ => appointments.OrderBy(a => a.DateTime),
            };

            return View(await appointments.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _context.Appointment
                .Include(a => a.Doctor).ThenInclude(d => d.Specialization)
                .Include(a => a.Patient)
                .Include(a => a.Service).ThenInclude(s => s.Specialization)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);

            if (appointment == null) return NotFound();

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "FullName");
            ViewData["DoctorId"] = BuildDoctorsSelectList();
            ViewData["ServiceId"] = BuildServicesSelectList();
            return View();
        }

        // POST: Appointments/Create (validare specializare)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,PatientId,DoctorId,ServiceId,DateTime,Status,Notes")] Appointment appointment)
        {
            var doctor = await _context.Doctor.AsNoTracking()
                .FirstOrDefaultAsync(d => d.DoctorId == appointment.DoctorId);

            var service = await _context.Service.AsNoTracking()
                .FirstOrDefaultAsync(s => s.ServiceId == appointment.ServiceId);

            if (doctor == null || service == null) return NotFound();

            if (doctor.SpecializationId == null || service.SpecializationId == null ||
                doctor.SpecializationId != service.SpecializationId)
            {
                ModelState.AddModelError(string.Empty,
                    "Medicul selectat nu are specializarea necesară pentru serviciul ales.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // repopulare dropdown la eroare
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "FullName", appointment.PatientId);
            ViewData["DoctorId"] = BuildDoctorsSelectList(appointment.DoctorId);
            ViewData["ServiceId"] = BuildServicesSelectList(appointment.ServiceId);

            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null) return NotFound();

            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "FullName", appointment.PatientId);
            ViewData["DoctorId"] = BuildDoctorsSelectList(appointment.DoctorId);
            ViewData["ServiceId"] = BuildServicesSelectList(appointment.ServiceId);

            return View(appointment);
        }

        // POST: Appointments/Edit/5 (validare specializare)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,PatientId,DoctorId,ServiceId,DateTime,Status,Notes")] Appointment appointment)
        {
            if (id != appointment.AppointmentId) return NotFound();

            var doctor = await _context.Doctor.AsNoTracking()
                .FirstOrDefaultAsync(d => d.DoctorId == appointment.DoctorId);

            var service = await _context.Service.AsNoTracking()
                .FirstOrDefaultAsync(s => s.ServiceId == appointment.ServiceId);

            if (doctor == null || service == null) return NotFound();

            if (doctor.SpecializationId == null || service.SpecializationId == null ||
                doctor.SpecializationId != service.SpecializationId)
            {
                ModelState.AddModelError(string.Empty,
                    "Medicul selectat nu are specializarea necesară pentru serviciul ales.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId)) return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "FullName", appointment.PatientId);
            ViewData["DoctorId"] = BuildDoctorsSelectList(appointment.DoctorId);
            ViewData["ServiceId"] = BuildServicesSelectList(appointment.ServiceId);

            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _context.Appointment
                .Include(a => a.Doctor).ThenInclude(d => d.Specialization)
                .Include(a => a.Patient)
                .Include(a => a.Service).ThenInclude(s => s.Specialization)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);

            if (appointment == null) return NotFound();

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointment.Remove(appointment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.AppointmentId == id);
        }
    }
}
