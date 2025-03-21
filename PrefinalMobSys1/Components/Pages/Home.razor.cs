using Microsoft.AspNetCore.Components;
using PrefinalMobSys1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Components.Pages
{
    public partial class Home:ComponentBase
    {
        [Inject]
        public AppShellContext AppShell { get; set; }

        [Inject]
        public NavigationManager Nav { get; set; }

        [Inject]
        public DatabaseContext DB { get; set; }

        /// <summary>
        /// This will be called on load or start of a page
        /// </summary>
        protected void Initialized()
        {
            //If Login Session is not set, return to Login Page
            if (!AppShell.IsUserLoggedIn)
            {
                Nav.NavigateTo("/login");
            }
        }
    }
}
