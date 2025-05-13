using Microsoft.AspNetCore.Components;
using PrefinalMobSys1.Data;
using PrefinalMobSys1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Components.Pages
{
    public partial class RecipeView : ComponentBase
    {
        [Inject]
        public AppShellContext AppShell { get; set; }

        [Inject]
        public NavigationManager Nav { get; set; }

        [Inject]
        public DatabaseContext DB { get; set; }

        [Parameter]
        [SupplyParameterFromQuery]
        public int? productid { get; set; }

        public RecipesViewModel Model { get; set; }

        protected override async void OnInitialized()
        {
            Model = new RecipesViewModel();

            if (Model.SelectedRecipe != null)
            {
                Model.LoadedPhoto = $"/ProductPhotos/{Model.SelectedRecipe.ID}.jpg";
            }
            else
            {
                Model.LoadedPhoto = $"/imgs/recommended/2.jpg";
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
    }
}
