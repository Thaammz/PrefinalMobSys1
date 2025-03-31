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

        public async Task<List<Product>> GetProducts()
        {
            //var originalProductList = await DB.Products();
            //var ProductList = new List<ProductItemModel>();

            //foreach(var itm in originalProductList)
            //{
            //    ProductItemModel newitm = new ProductItemModel()
            //    {
            //        ID = itm.ID,
            //        Name = itm.Name,
            //        Category = itm.Category,
            //        Description = itm.Description,
            //        Price = itm.Price,
            //        SKU = itm.SKU,
            //        IsActive = itm.IsActive,
            //        IsDeleted = itm.IsDeleted,
            //        CreatedBy = itm.CreatedBy,
            //        ModifiedBy = itm.ModifiedBy,
            //        CreatedDate = itm.CreatedDate,
            //        ModifiedDate = itm.ModifiedDate,
            //        Icon = GetIconFromCategory(itm.Category)
            //    };
            //    ProductList.Add(newitm);
            //}


            //return ProductList;
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
                Model.SelectedProduct.SKU = string.IsNullOrWhiteSpace(Model.SelectedProduct.SKU) ? DateTime.Now.Ticks.ToString() : Model.SelectedProduct.SKU;
                await DB.SaveProduct(Model.SelectedProduct);
                Model.ShowForm = false;
                Model.Status = "success";
                Model.StatusMessage = "Product changes has been saved successfully!";
                Model.Products = await GetProducts();
            }
            await InvokeAsync(StateHasChanged);
        }

        public async void LoadProduct(int ProductID)
        {
            Model.SelectedProduct = (from row in Model.Products where row.ID == ProductID select row).FirstOrDefault();
            Model.LoadedPhotoPath = $"/ProductPhotos/{ProductID}.jpg";
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

        public string GetIconFromCategory(string cat)
        {
            string resp = "";
            switch (cat)
            {
                case "Hamburgers":
                    resp = "fa-hamburger";
                    break;
                case "Pizza":
                    resp = "fa-pizza-slice";
                    break;
                case "Hotdogs":
                    resp = "fa-hotdog";
                    break;
                case "Cookies":
                    resp = "fa-cookie-bite";
                    break;
                case "IceCream":
                    resp = "fa-ice-cream";
                    break;
            }
            return resp;
        }

        public async void SearchTerm(ChangeEventArgs e)
        {
            var items = await GetProducts();
            string searchTerm = e.Value.ToString().ToLower();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                
                var searchResults = (from row in items
                                     where row.Name.ToLower().Contains(searchTerm)
                                     || row.Description.ToLower().Contains(searchTerm)
                                     select row).ToList();
                Model.Products = searchResults;
            }
            else
            {
                Model.Products = items;
            }

            await InvokeAsync(StateHasChanged);//refresh rendered page
        }

        public async void AddProductPhoto(int productID)
        {
            string folderPath = Path.Combine(FileSystem.AppDataDirectory, "ProductPhotos");
            string retFile = await DeviceUtilities.AddPhoto(folderPath,$"{productID}.jpg");

            if (!string.IsNullOrWhiteSpace(retFile))
            {
                string filenameOnly = Path.GetFileName(retFile);
                Model.LoadedPhotoPath = $"/ProductPhotos/{filenameOnly}";
                await InvokeAsync(StateHasChanged);//refresh rendered page
            }
        }
    }
}
