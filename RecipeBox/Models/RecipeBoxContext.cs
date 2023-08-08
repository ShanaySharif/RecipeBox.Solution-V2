using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace RecipeBox.Models
{
    public class RecipeBoxContext : IdentityDbContext<Account>
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<RecipeTag> RecipeTags { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public RecipeBoxContext(DbContextOptions options) : base(options) { }
    }
   
 }   


