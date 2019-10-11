using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RPSurvey3.Data;
using RPSurvey3.Models.ViewModels;

namespace RPSurvey3.Models
{
    [Area("Admin")]
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnvironment;

        [BindProperty]
        public QuestionViewModel QuestionVM { get; set; }

        public QuestionController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            QuestionVM = new QuestionViewModel()
            {
                Section = _db.Section,
                Question = new Models.Question()
            };
        }

        //Get--Index view
        public async Task<IActionResult> Index()

        {
            var questions = await _db.Question
                .Include(m => m.Section)
                .OrderBy(m => m.Section.DisplayOrder)
                .Include(m => m.SubSection)
                .OrderBy(m => m.SubSection.DisplayOrder)
                .ToListAsync();

            return View(questions);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View(QuestionVM);
        }

        //Post-Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            QuestionVM.Question.SubSectionId = Convert.ToInt32(Request.Form["SubSectionId"].ToString());

            if (!ModelState.IsValid)
            {
                return View(QuestionVM);
            }

            _db.Question.Add(QuestionVM.Question);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //===================================================================
        //GET - Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            QuestionVM.Question = await _db.Question.Include(m => m.Section).Include(m => m.SubSection).SingleOrDefaultAsync(m => m.Id == id);
            QuestionVM.SubSection = await _db.SubSection.Where(s => s.SectionId == QuestionVM.Question.SectionId).ToListAsync();

            if (QuestionVM.Question == null)
            {
                return NotFound();
            }
            return View(QuestionVM);
        }

        //Post-Edit
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            QuestionVM.Question.SubSectionId = Convert.ToInt32(Request.Form["SubSectionId"].ToString());

            if (!ModelState.IsValid)
            {
                QuestionVM.SubSection = await _db.SubSection.Where(s => s.SectionId == QuestionVM.Question.SectionId).ToListAsync();

                return View(QuestionVM);
            }

            var questionFromDb = await _db.Question.FindAsync(QuestionVM.Question.Id);

            questionFromDb.SurveyQuestion = QuestionVM.Question.SurveyQuestion;
            questionFromDb.SectionId = QuestionVM.Question.SectionId;
            questionFromDb.SubSectionId = QuestionVM.Question.SubSectionId;
            questionFromDb.DisplayOrder = QuestionVM.Question.DisplayOrder;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //GET : Details Question
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            QuestionVM.Question = await _db.Question.Include(m => m.Section).Include(m => m.SubSection).SingleOrDefaultAsync(m => m.Id == id);

            if (QuestionVM.Question == null)
            {
                return NotFound();
            }

            return View(QuestionVM);
        }

        //GET : Delete Question
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            QuestionVM.Question = await _db.Question.Include(m => m.Section).Include(m => m.SubSection).SingleOrDefaultAsync(m => m.Id == id);

            if (QuestionVM.Question == null)
            {
                return NotFound();
            }

            return View(QuestionVM);
        }

        //POST Delete Question
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Question question = await _db.Question.FindAsync(id);

            if (question != null)
            {
                _db.Question.Remove(question);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}