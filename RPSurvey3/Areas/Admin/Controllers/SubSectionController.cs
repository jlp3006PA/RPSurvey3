using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPSurvey3.Data;
using RPSurvey3.Models;
using RPSurvey3.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPSurvey3.Areas.Admin.Controllers
{
    [Area("Admin")]
    //  [Authorize(Roles = SD.ManagerUser)]
    public class SubSectionController : Controller
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        public string StatusMessage { get; set; }

        public SubSectionController(ApplicationDbContext db)
        {
            _db = db;
        }

        //Get Index
        public async Task<IActionResult> Index()
        {
            var subSections = await _db.SubSection.Include(s => s.Section).ToListAsync();

            return View(subSections);
        }

        //Get Create
        public async Task<IActionResult> Create()
        {
            SubSectionAndSectionViewModel model = new SubSectionAndSectionViewModel()
            {
                SectionList = await _db.Section.ToListAsync(),
                SubSection = new Models.SubSection(),
                SubSectionList = await _db.SubSection.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync(),
            };

            return View(model);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubSectionAndSectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesSubSectionExists = _db.SubSection.Include(s => s.Section).Where(s => s.Name == model.SubSection.Name && s.Section.Id == model.SubSection.SectionId);

                if (doesSubSectionExists.Count() > 0)
                {
                    //Error
                    //StatusMessage = "Error : Sub Section exists under " + doesSubSectionExists.First().Section.Name + " category. Please use another name.";
                }
                else
                {
                    _db.SubSection.Add(model.SubSection);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            SubSectionAndSectionViewModel modelVM = new SubSectionAndSectionViewModel()
            {
                SectionList = await _db.Section.ToListAsync(),
                SubSection = model.SubSection,
                SubSectionList = await _db.SubSection.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync(),
                //StatusMessage = StatusMessage
            };
            return View(modelVM);
        }

        [ActionName("GetSubSection")]
        public async Task<IActionResult> GetSubSection(int id)
        {
            List<SubSection> subCategories = new List<SubSection>();

            subCategories = await (from SubSection in _db.SubSection
                                   where SubSection.SectionId == id
                                   select SubSection).ToListAsync();
            return Json(new SelectList(subCategories, "Id", "Name"));
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SubSection = await _db.SubSection.SingleOrDefaultAsync(m => m.Id == id);

            if (SubSection == null)
            {
                return NotFound();
            }

            SubSectionAndSectionViewModel model = new SubSectionAndSectionViewModel()
            {
                SectionList = await _db.Section.ToListAsync(),
                SubSection = SubSection,
                SubSectionList = await _db.SubSection.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync()
            };

            return View(model);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SubSectionAndSectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesSubSectionExists = _db.SubSection.Include(s => s.Section).Where(s => s.Name == model.SubSection.Name && s.Section.Id == model.SubSection.SectionId);

                if (doesSubSectionExists.Count() > 0)
                {
                    //Error
                    StatusMessage = "Error : SubSection exists under " + doesSubSectionExists.First().Section.Name + " category. Please use another name.";
                }
                else
                {
                    var subCatFromDb = await _db.SubSection.FindAsync(model.SubSection.Id);
                    subCatFromDb.Name = model.SubSection.Name;

                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            SubSectionAndSectionViewModel modelVM = new SubSectionAndSectionViewModel()
            {
                SectionList = await _db.Section.ToListAsync(),
                SubSection = model.SubSection,
                SubSectionList = await _db.SubSection.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync(),
                StatusMessage = StatusMessage
            };
            //modelVM.SubSection.Id = id;
            return View(modelVM);
        }

        //GET Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var SubSection = await _db.SubSection.Include(s => s.Section).SingleOrDefaultAsync(m => m.Id == id);
            if (SubSection == null)
            {
                return NotFound();
            }

            return View(SubSection);
        }

        //GET Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var SubSection = await _db.SubSection.Include(s => s.Section).SingleOrDefaultAsync(m => m.Id == id);
            if (SubSection == null)
            {
                return NotFound();
            }

            return View(SubSection);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var SubSection = await _db.SubSection.SingleOrDefaultAsync(m => m.Id == id);
            _db.SubSection.Remove(SubSection);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}