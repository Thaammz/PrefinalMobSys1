using Microsoft.AspNetCore.Components;
using PrefinalMobSys1.Data;
using PrefinalMobSys1.Models;
using PrefinalMobSys1.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Components.Pages
{
    public partial class RecipeEditor : ComponentBase
    {
        [Inject]
        public AppShellContext AppShell { get; set; }

        [Inject]
        public NavigationManager Nav { get; set; }

        [Inject]
        public DatabaseContext DB { get; set; }

        [Parameter]
        [SupplyParameterFromQuery]
        public int? recipeid { get; set; }

        public RecipesViewModel Model { get; set; }

        protected override async void OnInitialized()
        {
            Model = new RecipesViewModel();
            Model.IsNew = !recipeid.HasValue;

            if (Model.IsNew)
            {
                Model.SelectedRecipe = new Recipe();
            }
            else
            {
                if (recipeid != null)
                {
                    await LoadRecipe(recipeid.Value);
                }                
            }

            await InvokeAsync(StateHasChanged);//refresh rendered page
        }

        public void AddServings()
        {
            Model.Servings++;

        }

        public void MinusServings()
        {
            if (Model.Servings > 0)
            {
                Model.Servings--;
            }


            //removes to cart if zero quantity
            if (Model.Servings == 0)
            {
                //UI to remove to cart
            }
        }

        public async void AddRecipePhoto(int RecipeID)
        {
            string folderPath = Path.Combine(FileSystem.AppDataDirectory, "RecipePhotos");
            string retFile = await DeviceUtilities.AddPhoto(folderPath, $"temp.jpg");

            if (!string.IsNullOrWhiteSpace(retFile))
            {
                string filenameOnly = Path.GetFileName(retFile);
                Model.LoadedPhoto = $"/RecipePhotos/{filenameOnly}";
                await InvokeAsync(StateHasChanged);//refresh rendered page
            }
        }

        public async void SaveRecipe()
        {
            var allrecipes = await DB.Recipes();
            
            if (string.IsNullOrWhiteSpace(Model.SelectedRecipe.Name))
            {
                Model.Status = "danger";
                Model.StatusMessage = "Recipename cannot be blank or only spaces!";
            }
            else if (
                allrecipes.Select(r => r.Name).ToList().Contains(Model.SelectedRecipe.Name)
                &&
                Model.IsNew)
            {
                Model.Status = "danger";
                Model.StatusMessage = "Recipe already exists!";
            }
            else
            {
                //Set loading gif to not locked image
                Model.LoadedPhoto = $"/imgs/loading.gif";

                Model.SelectedRecipe.UserID = AppShell.CurrentUser.ID;
                Model.SelectedRecipe.ModifiedBy = AppShell.CurrentUser.Username;
                Model.SelectedRecipe.ModifiedDate = DateTime.Now;
                if (Model.IsNew)
                {
                    Model.SelectedRecipe.CreatedBy = AppShell.CurrentUser.Username;
                    Model.SelectedRecipe.CreatedDate = DateTime.Now;
                }

                await DB.SaveRecipe(Model.SelectedRecipe);

                //Post Changes
                //Get the stored ID after saving a new record
                allrecipes = await DB.Recipes();
                var storedRec = (from rw in allrecipes where rw.Name == Model.SelectedRecipe.Name select rw).FirstOrDefault();
                string tempImage = $"{FileSystem.AppDataDirectory}/RecipePhotos/temp.jpg";
                if (File.Exists(tempImage) && storedRec != null)
                {
                    string targetImage = $"{FileSystem.AppDataDirectory}/RecipePhotos/{storedRec.ID}.jpg";
                    File.Copy(tempImage, targetImage);
                    Model.LoadedPhoto = "/RecipePhotos/{storedRec.ID}.jpg";
                    //await InvokeAsync(StateHasChanged);
                    // Enclose with Try just incase File is not deletable at the moment
                    try { File.Delete(tempImage); } catch (Exception err) { }

                    Model.LoadedPhoto = $"/RecipePhotos/{storedRec.ID}.jpg";
                    Model.Status = "success";
                    Model.StatusMessage = "Recipe changes has been saved successfully!";
                }
                
            }
            await InvokeAsync(StateHasChanged);
        }

        public async Task LoadRecipe(int RecipeID)
        {
            var allrecipes = await DB.Recipes();
            Model.SelectedRecipe = (from row in allrecipes where row.ID == RecipeID select row).FirstOrDefault();
            Model.LoadedPhoto = $"/RecipePhotos/{RecipeID}.jpg";
            if(Model.SelectedRecipe == null)
            {
                Model.SelectedRecipe = new Recipe();
            }

            await InvokeAsync(StateHasChanged);//refresh rendered page
        }

        public async void DeleteRecipe(int Recipeid)
        {
            var selRecipe = (from row in Model.Recipes where row.ID == Recipeid select row).FirstOrDefault();
            if (selRecipe != null)
            {
                await DB.DeleteRecipe(selRecipe);
                Model.Status = "success";
                Model.StatusMessage = "Recipe has been deleted successfully!";
                //Model.Recipes = await GetRecipes();
                await InvokeAsync(StateHasChanged);
            }
        }
    }
}
