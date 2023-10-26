using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeAppAPI;
using RecipeAppAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeAppAPI.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly RecipeDataContext _context;

        public RecipeController(RecipeDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllRecipe")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetAllRecipes()
        {
            var recipes = await _context.Recipes.Include(r => r.Ingredients).ToListAsync();
            return Ok(recipes);
        }

        [HttpPost]
        [Route("CreateRecipe")]
        public async Task<ActionResult<Recipe>> CreateRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRecipe", new { id = recipe.RecipeId }, recipe);
        }

        [HttpGet]
        [Route("GetRecipe/{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.Include(r => r.Ingredients).FirstOrDefaultAsync(r => r.RecipeId == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

        [HttpPut]
        [Route("UpdateRecipe/{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, Recipe recipe)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteRecipe/{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {

            var recipe = await _context.Recipes
           .Include(r => r.Ingredients)
           .FirstOrDefaultAsync(r => r.RecipeId == id);

                if (recipe == null)
                {
                    return NotFound();
                }

                // Delete related ingredients first
                _context.Ingredients.RemoveRange(recipe.Ingredients);

                // Then delete the recipe
                _context.Recipes.Remove(recipe);

                await _context.SaveChangesAsync();

                return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.RecipeId == id);
        }
    }
}
