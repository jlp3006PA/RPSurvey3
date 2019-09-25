using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPSurvey3.Data;
using RPSurvey3.Models;

namespace RPSurvey3.Areas.Admin.Controllers
{
    //[Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]
    public class SectionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SectionController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET
        public async Task<IActionResult> Index()
        {
            return View(await _db.Section.ToListAsync());
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Section section)
        {
            if (ModelState.IsValid)
            {
                //if valid
                _db.Section.Add(section);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(section);
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var section = await _db.Section.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        //Edit Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Section section)
        {
            if (ModelState.IsValid)
            {
                _db.Update(section);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(section);
        }

        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var section = await _db.Section.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            return View(section);
        }

        //GET --  Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var section = await _db.Section.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);

        }

    }
}