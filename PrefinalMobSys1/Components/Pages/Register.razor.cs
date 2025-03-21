using Microsoft.AspNetCore.Components;
using PrefinalMobSys1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Components.Pages
{
    public partial class Register : ComponentBase
    {
        public string Status = "";
        public string StatusMessage = "";


        /// <summary>
        /// This injects a preloaded copy of NavigationManager from MauiProgram.cs
        /// </summary>
        [Inject]
        public NavigationManager Nav { set; get; }

        /// <summary>
        /// This injects a preloaded copy of DatabaseContext from MauiProgram.cs
        /// </summary>
        [Inject]
        public DatabaseContext DB { set; get; }

        /// <summary>
        /// User model bound to the page
        /// </summary>
        public Models.User Model = new Models.User();

        public async void RegisterUser()
        {
            Model.IsDeleted = false;
            Model.CreatedBy = "SYSTEM";
            Model.ModifiedBy = "SYSTEM";
            Model.CreatedDate = DateTime.Now;
            Model.ModifiedDate = DateTime.Now;            

            await DB.SaveUser(Model);
            Status = "success";
            StatusMessage = "User changes has been saved successfully!";

            await InvokeAsync(StateHasChanged);
        }
    }
}
