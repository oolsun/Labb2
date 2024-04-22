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
    public class SubjectsController : Controller
    {
        private readonly Labb2DbContext _context;

        public SubjectsController(Labb2DbContext context)
        {
            _context = context;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            var labb2DbContext = _context.Subjects
                                        .Include(s => s.Teacher);

            return View(await labb2DbContext.ToListAsync());
        }

        public IActionResult Details()
        {
            var enrollments = _context.Enrollments
                                      .Include(e => e.Student)
                                      .Include(e => e.Subject)
                                          .ThenInclude(s => s.Teacher)
                                      .Where(e => e.Subject.SubjectName == "Programmering 1" &&
                                                  e.Subject.Teacher.TeacherName == "Reidar")
                                      .ToList();

            var allStudents = _context.Enrollments
                                      .Include(e => e.Student)
                                      .Include(e => e.Subject)
                                          .ThenInclude(s => s.Teacher)
                                      .Where(e => e.Subject.SubjectName == "Programmering 1")
                                      .ToList();

            var uniqueStudents = enrollments.Select(e => e.Student).Distinct().ToList();

            var studentList = new SelectList(uniqueStudents, "StudentId", "StudentName");

            ViewBag.StudentList = studentList;

            ViewBag.Enrollments = allStudents;

            return View();
        }

        public IActionResult ChangeTeacher(int studentId)
        {
            var studentEnrollments = _context.Enrollments
                                              .Include(e => e.Subject)
                                                  .ThenInclude(s => s.Teacher)
                                              .Where(e => e.Student.StudentId == studentId)
                                              .ToList();

            foreach (var enrollment in studentEnrollments)
            {
                if (enrollment.Subject.SubjectName == "Programmering 1" && enrollment.Subject.Teacher.TeacherName == "Reidar")
                {
                    var subjectTobias = _context.Subjects
                                                  .Include(s => s.Teacher)
                                                  .FirstOrDefault(s => s.SubjectName == "Programmering 1" && s.Teacher.TeacherName == "Tobias");

                    if (subjectTobias != null)
                    {
                        enrollment.Subject = subjectTobias;
                    }
                    else
                    {
                        return NotFound("Kursen Programmering 1 med läraren Tobias hittades inte i systemet.");
                    }
                }
            }
            _context.SaveChanges();

            return RedirectToAction("Details", "Subjects");
        }


        // GET: Subjects/Create
        public IActionResult Create()
        {
            ViewData["FK_TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId");
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,SubjectName,FK_TeacherId")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", subject.FK_TeacherId);
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit()
        {
            var subject = await _context.Subjects
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(s => s.SubjectName == "Programmering 2" || s.SubjectName == "OOP");

            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeSubjectName(int id, string newSubjectName)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            subject.SubjectName = newSubjectName;

            await _context.SaveChangesAsync();

            return RedirectToAction("Edit");
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.SubjectId == id);
        }


    }
}
