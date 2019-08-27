using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RPSurvey3.Data;

namespace RPSurvey3.Models
{
    [Area("Admin")]
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnvironment;

        public QuestionController(ApplicationDbContext db,
                                  IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()

        {
            var questions = await _db.Question.Include(m => m.Section).Include(m => m.SubSection).ToListAsync();
            return View(questions);
        }
    }
}