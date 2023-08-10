using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeBox.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using RecipeBox.ViewModels;

namespace RecipeBox.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        private readonly RecipeBoxContext _db;
        private readonly UserManager<Account> _userManager;

        public TagsController(UserManager<Account> userManager, RecipeBoxContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Tags.ToList());
        }

        public ActionResult Details(int id)
        {
            Tag thisTag = _db.Tags
                .Include(tag => tag.JoinEntities)
                .ThenInclude(join => join.Recipe)
                .FirstOrDefault(thisTag => thisTag.TagId == id);
            return View(thisTag);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Tag tag)
        {
            _db.Tags.Add(tag);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddRecipe(int id)
        {
            Tag thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
            ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "RecipeName");
            return View(thisTag);
        }

        [HttpPost]
        public ActionResult AddRecipe(Tag tag, int recipeId)
        {
            RecipeTag joinEntity = _db.RecipeTags.FirstOrDefault(join => join.RecipeId == recipeId && join.TagId == tag.TagId);
            if (joinEntity == null && recipeId != 0)
            {
                _db.RecipeTags.Add(new RecipeTag() { RecipeId = recipeId, TagId = tag.TagId });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = tag.TagId });
        }

        public ActionResult Edit(int id)
        {
            Tag thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
            return View(thisTag);
        }

        [HttpPost]
        public ActionResult Edit(Tag tag)
        {
            _db.Tags.Update(tag);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Tag thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
            return View(thisTag);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Tag thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
            _db.Tags.Remove(thisTag);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteJoin(int joinId)
        {
            RecipeTag joinEntry = _db.RecipeTags.FirstOrDefault(entry => entry.RecipeTagId == joinId);
            _db.RecipeTags.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
