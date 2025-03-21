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
    public partial class Products:ComponentBase
    {
        [Inject]
        public DatabaseContext DB { set; get; }

        public ProductsViewModel Model { get; set; }

        protected override async void OnInitialized()
        {
            Model = new ProductsViewModel();
            Model.Products = await GetProducts();
            await InvokeAsync(StateHasChanged);//refresh rendered page
            //return Task.Delay(0);
        }

        public async Task<List<Models.Product>> GetProducts()
        {
            return await DB.Products();
        }

        public async void SaveProduct()
        {
            if (string.IsNullOrWhiteSpace(Model.SelectedProduct.Name))
            {
                Model.Status = "danger";
                Model.StatusMessage = "Productname cannot be blank or only spaces!";
            }
            else if (
                Model.Products.Select(r => r.Name).ToList().Contains(Model.SelectedProduct.Name)
                &&
                Model.IsNew)
            {
                Model.Status = "danger";
                Model.StatusMessage = "Product already exists!";
            }
            else
            {
                await DB.SaveProduct(Model.SelectedProduct);
                Model.ShowForm = false;
                Model.Status = "success";
                Model.StatusMessage = "Product changes has been saved successfully!";
                Model.Products = await GetProducts();
            }
            await InvokeAsync(StateHasChanged);
        }

        public async void LoadProduct(int Productid)
        {
            Model.SelectedProduct = (from row in Model.Products where row.ID == Productid select row).FirstOrDefault();
            Model.ShowForm = true;
            Model.IsNew = false;
            await InvokeAsync(StateHasChanged);//refresh rendered page
        }

        public async void DeleteProduct(int Productid)
        {
            var selProduct = (from row in Model.Products where row.ID == Productid select row).FirstOrDefault();
            if (selProduct != null)
            {
                await DB.DeleteProduct(selProduct);
                Model.Status = "success";
                Model.StatusMessage = "Product has been deleted successfully!";
                Model.Products = await GetProducts();
                await InvokeAsync(StateHasChanged);
            }
        }

        public void AddProduct()
        {
            Model.StatusMessage = ""; //clear alert
            Model.SelectedProduct = new Models.Product();
            Model.IsNew = true;
            ShowProductForm();
        }

        public void ShowProductForm()
        {
            Model.ShowForm = true;
        }
    }
}
