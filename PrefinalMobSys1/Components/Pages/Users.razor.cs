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
    public partial class Users:ComponentBase
    {
        [Inject]
        public DatabaseContext DB { set; get; }

        public UsersViewModel Model { get; set; }

        protected override async void OnInitialized()
        {
            Model = new UsersViewModel();
            Model.Users = await GetUsers();
            await InvokeAsync(StateHasChanged);//refresh rendered page
            //return Task.Delay(0);
        }

        public async Task<List<Models.User>> GetUsers()
        {
            return await DB.Users();
        }

        public async void SaveUser()
        {
            if (string.IsNullOrWhiteSpace(Model.SelectedUser.Username))
            {
                Model.Status = "danger";
                Model.StatusMessage = "Username cannot be blank or only spaces!";
            }
            else if (
                Model.Users.Select(r => r.Username).ToList().Contains(Model.SelectedUser.Username)
                &&
                Model.IsNew)
            {
                Model.Status = "danger";
                Model.StatusMessage = "User already exists!";
            }
            else
            {
                await DB.SaveUser(Model.SelectedUser);
                Model.ShowForm = false;
                Model.Status = "success";
                Model.StatusMessage = "User changes has been saved successfully!";
                Model.Users = await GetUsers();
            }
            await InvokeAsync(StateHasChanged);
        }

        public async void LoadUser(int userid)
        {
            Model.SelectedUser = (from row in Model.Users where row.ID == userid select row).FirstOrDefault();
            Model.ShowForm = true;
            Model.IsNew = false;
            await InvokeAsync(StateHasChanged);//refresh rendered page
        }

        public async void DeleteUser(int userid)
        {
            var selUser = (from row in Model.Users where row.ID == userid select row).FirstOrDefault();
            if (selUser != null)
            {
                await DB.DeleteUser(selUser);
                Model.Status = "success";
                Model.StatusMessage = "User has been deleted successfully!";
                Model.Users = await GetUsers();
                await InvokeAsync(StateHasChanged);
            }                        
        }

        public void AddUser()
        {
            Model.StatusMessage = ""; //clear alert
            Model.SelectedUser = new Models.User();
            Model.IsNew = true;
            ShowUserForm();
        }

        public void ShowUserForm()
        {
            Model.ShowForm = true;
        }
    }
}
