using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace RecipeBox.Controllers
{
    public class HomeController : Controller
    {
        private readonly RecipeBoxContext _db;
        private readonly UserManager<Account> _userManager;

        public HomeController(UserManager<Account> userManager, RecipeBoxContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        [HttpGet("/")]
        public async Task<ActionResult> Index()
        {
            Dictionary<string, object[]> model = new Dictionary<string, object[]>();

            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Account currentUser = await _userManager.FindByIdAsync(userId);
            if (currentUser != null)
            {
                Recipe[] recipes = _db.Recipes
                    .Where(entry => entry.User.Id == currentUser.Id)
                    .ToArray();
                model.Add("recipes", recipes);
            }
            return View(model);
        }
    }

}