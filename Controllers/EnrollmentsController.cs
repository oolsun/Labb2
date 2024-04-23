using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb2.Data;
using Labb2.Models;

namespace Labb2.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly Labb2DbContext _context;

        public EnrollmentsController(Labb2DbContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> StudentsInProgramming()
        {
            var labb2DbContext = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .ThenInclude(t => t.Teacher)
                .Where(s => s.Subject.SubjectName == "Programmering 1");

            return View(await labb2DbContext.ToListAsync());
        }

        // GET: Enrollments
        public async Task<IActionResult> AllStudentsWithTeachers()
        {
            var labb2DbContext = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .ThenInclude(t => t.Teacher);
            return View(await labb2DbContext.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            ViewData["FK_SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentId,Grade,FK_SubjectId,FK_StudentId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", enrollment.FK_StudentId);
            ViewData["FK_SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", enrollment.FK_SubjectId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", enrollment.FK_StudentId);
            ViewData["FK_SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", enrollment.FK_SubjectId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentId,Grade,FK_SubjectId,FK_StudentId")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentId))
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
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", enrollment.FK_StudentId);
            ViewData["FK_SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", enrollment.FK_SubjectId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentId == id);
        }
    }
}
