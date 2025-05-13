using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class RecipesViewModel: BaseViewModel
    {
        /// <summary>
        /// To Identify if the form is new or an update
        /// </summary>
        /// 
        public bool SelectMode { get; set; } = false;
        public int Servings { get; set; }
        public string LoadedPhoto { get; set; }
        public Recipe SelectedRecipe { get; set; } 
        public List<RecipeIngredient> Ingredients { get; set; }
        public List<Recipe> CookingSteps { get; set; } 
        public List<Recipe> Recipes { get; set; }


        public RecipesViewModel()
        {
            Ingredients = new List<RecipeIngredient>();
            CookingSteps = new List<Recipe>();
            Recipes = new List<Recipe>();
            SelectedRecipe = new Recipe();
            SelectMode = false;
        }
    }
}
