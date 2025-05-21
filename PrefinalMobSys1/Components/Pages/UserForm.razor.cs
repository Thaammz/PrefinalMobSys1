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
    public partial class UserForm : ComponentBase
    {
        [Inject]
        public AppShellContext AppShell { get; set; }

        [Inject]
        public NavigationManager Nav { get; set; }

        [Inject]
        public DatabaseContext DB { get; set; }

        [Parameter]
        [SupplyParameterFromQuery]
        public int? userid { get; set; }

        public UsersViewModel Model { get; set; }

        protected override async void OnInitialized()
        {
            Model = new UsersViewModel();
            Model.IsNew = !userid.HasValue;

            if (Model.IsNew)
            {
                Model.SelectedUser = new User();
            }
            else
            {
                if (userid != null)
                {
                    await LoadUser(userid.Value);
                }
            }

            await InvokeAsync(StateHasChanged); // refresh rendered page
        }

        public async void SaveUser()
        {
            var allUsers = await DB.Users();

            if (string.IsNullOrWhiteSpace(Model.SelectedUser.Username))
            {
                Model.Status = "danger";
                Model.StatusMessage = "Username cannot be blank or only spaces!";
            }
            else if (
                allUsers.Select(u => u.Username).ToList().Contains(Model.SelectedUser.Username)
                && Model.IsNew)
            {
                Model.Status = "danger";
                Model.StatusMessage = "Username already exists!";
            }
            else
            {
                Model.SelectedUser.ModifiedBy = AppShell.CurrentUser?.Username;
                Model.SelectedUser.ModifiedDate = DateTime.Now;
                if (Model.IsNew)
                {
                    Model.SelectedUser.CreatedBy = AppShell.CurrentUser?.Username;
                    Model.SelectedUser.CreatedDate = DateTime.Now;
                }

                await DB.SaveUser(Model.SelectedUser);

                Model.Status = "success";
                Model.StatusMessage = "User changes have been saved successfully!";
            }
            await InvokeAsync(StateHasChanged);
        }

        public async Task LoadUser(int userId)
        {
            var allUsers = await DB.Users();
            Model.SelectedUser = allUsers.FirstOrDefault(u => u.ID == userId);
            if (Model.SelectedUser == null)
            {
                Model.SelectedUser = new User();
            }

            await InvokeAsync(StateHasChanged); // refresh rendered page
        }

        public async void DeleteUser(int userId)
        {
            var selUser = Model.Users?.FirstOrDefault(u => u.ID == userId);
            if (selUser != null)
            {
                await DB.DeleteUser(selUser);
                Model.Status = "success";
                Model.StatusMessage = "User has been deleted successfully!";
                await InvokeAsync(StateHasChanged);
            }
        }
    }
}
