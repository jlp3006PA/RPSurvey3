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

        public QuestionController(ApplicationDbContext db,
                                  IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            QuestionVM = new QuestionViewModel()
            {
                Section = _db.Section,
                Question = new Models.Question()
            };
        }

        public async Task<IActionResult> Index()

        {
            var questions = await _db.Question.Include(m => m.Section)
                .OrderBy(m => m.Section)
                .Include(m => m.SubSection)
                .ToListAsync();

            return View(questions);
        }

        //Get - Create
        //GET - CREATE
        public IActionResult Create()
        {
            return View(QuestionVM);
        }

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

            //Work on the image saving section

            //string webRootPath = _hostingEnvironment.WebRootPath;
            //var files = HttpContext.Request.Form.Files;

            //var questionFromDb = await _db.Question.FindAsync(QuestionVM.Question.Id);

            //if (files.Count > 0)
            //{
            //    //files has been uploaded
            //    var uploads = Path.Combine(webRootPath, "images");
            //    var extension = Path.GetExtension(files[0].FileName);

            //    using (var filesStream = new FileStream(Path.Combine(uploads, QuestionVM.Question.Id + extension), FileMode.Create))
            //    {
            //        files[0].CopyTo(filesStream);
            //    }
            //    questionFromDb.Image = @"\images\" + QuestionVM.Question.Id + extension;
            //}
            //else
            //{
            //    //no file was uploaded, so use default
            //    var uploads = Path.Combine(webRootPath, @"images\" + SD.DefaultFoodImage);
            //    System.IO.File.Copy(uploads, webRootPath + @"\images\" + QuestionVM.Question.Id + ".png");
            //    questionFromDb.Image = @"\images\" + QuestionVM.Question.Id + ".png";
            //}

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}