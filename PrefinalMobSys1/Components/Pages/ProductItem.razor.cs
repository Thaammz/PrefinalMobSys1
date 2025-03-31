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
    public partial class ProductItem:ComponentBase
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

        public ProductItemViewModel Model { get; set; }

        protected override async void OnInitialized()
        {
            Model = new ProductItemViewModel();
            Model.Item = new Product();

            var allproducts = await DB.Products();

            if (allproducts != null)
            {
                Model.Item = (from row in allproducts select row).FirstOrDefault();

                if (productid != null)
                {
                    Model.Item = (from row in allproducts where row.ID == productid.Value select row).FirstOrDefault();
                }
            }
            

            await InvokeAsync(StateHasChanged);//refresh rendered page
        }
    }
}
