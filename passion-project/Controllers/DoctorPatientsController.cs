using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using passion_project.Model;
using passion_project.Models.AppointmentSystem;

namespace passion_project.Controllers
{
    public class DoctorPatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorPatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DoctorPatients
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DoctorPatient.Include(d => d.Doctor).Include(d => d.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DoctorPatients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorPatient = await _context.DoctorPatient
                .Include(d => d.Doctor)
                .Include(d => d.Patient)
                .FirstOrDefaultAsync(m => m.DoctorPatientId == id);
            if (doctorPatient == null)
            {
                return NotFound();
            }

            return View(doctorPatient);
        }

        // GET: DoctorPatients/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorId");
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientId");
            return View();
        }

        // POST: DoctorPatients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorPatientId,DoctorId,PatientId")] DoctorPatient doctorPatient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorPatient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorId", doctorPatient.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientId", doctorPatient.PatientId);
            return View(doctorPatient);
        }

        // GET: DoctorPatients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorPatient = await _context.DoctorPatient.FindAsync(id);
            if (doctorPatient == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorId", doctorPatient.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientId", doctorPatient.PatientId);
            return View(doctorPatient);
        }

        // POST: DoctorPatients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorPatientId,DoctorId,PatientId")] DoctorPatient doctorPatient)
        {
            if (id != doctorPatient.DoctorPatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctorPatient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorPatientExists(doctorPatient.DoctorPatientId))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "DoctorId", doctorPatient.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientId", doctorPatient.PatientId);
            return View(doctorPatient);
        }

        // GET: DoctorPatients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorPatient = await _context.DoctorPatient
                .Include(d => d.Doctor)
                .Include(d => d.Patient)
                .FirstOrDefaultAsync(m => m.DoctorPatientId == id);
            if (doctorPatient == null)
            {
                return NotFound();
            }

            return View(doctorPatient);
        }

        // POST: DoctorPatients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctorPatient = await _context.DoctorPatient.FindAsync(id);
            _context.DoctorPatient.Remove(doctorPatient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorPatientExists(int id)
        {
            return _context.DoctorPatient.Any(e => e.DoctorPatientId == id);
        }
    }
}
