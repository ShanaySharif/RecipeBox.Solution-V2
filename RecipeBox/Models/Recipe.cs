using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
  public class Recipe
  {
    public int RecipeId { get; set; }
    [Required(ErrorMessage = "This field cannot be empty. Please try again.")]
    public string RecipeName { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public List <RecipeTag> JoinEntities { get; set; }
    public Account User { get; set; }   
    
  }
}