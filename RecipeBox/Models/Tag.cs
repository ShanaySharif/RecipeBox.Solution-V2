using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
    public class Tag
    {
        public int TagId { get; set; }

        [Required(ErrorMessage = "This field cannot be empty. Please try again.")]
        public string Title { get; set; }
        public List<RecipeTag> JoinEntities { get; set; }

        public Account User { get; set; }
    }
}
