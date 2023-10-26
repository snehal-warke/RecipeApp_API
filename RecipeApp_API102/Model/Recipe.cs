using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeAppAPI
{
    public class Recipe
    {
        public int RecipeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImagePath { get; set; }

        // Define a navigation property to represent the related ingredients.
        public List<Ingredient> Ingredients { get; set; }
    }

    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
