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
    public partial class MyCart:ComponentBase
    {
        [Inject]
        public AppShellContext AppShell { get; set; }

        [Inject]
        public NavigationManager Nav { get; set; }

        [Inject]
        public DatabaseContext DB { get; set; }

        public CartViewModel Model { get; set; }

        public const double DeliveryFee = 10.00;
        /// <summary>
        /// This will be called on load or start of a page
        /// </summary>
        protected override async void OnInitialized()
        {
            Model = new CartViewModel();
            Model.Items4Checkout = new List<CartItemViewModel>();

            if (AppShell.CurrentUser != null)
            {
                int userID = AppShell.CurrentUser.ID;
                List<Cart> orders = await DB.Carts();
                List<CartItem> orderedItems = await DB.CartItems();
                List<Product> allproducts = await DB.Products();
                Cart? order = null;
                if (orders != null)
                {
                    order = (from r in orders
                             where r.UserID == userID
                             && !r.IsPaid
                             && !r.IsCompleted
                             select r
                         ).FirstOrDefault();
                    if (order != null)
                    {
                        Model.Order = order;
                        var ordereditems = (from r in orderedItems
                                            where r.CartID == userID
                                            select r).ToList();
                        foreach (var row in ordereditems)
                        {
                            var refProd = (from r in allproducts
                                           where r.ID == row.ProductID
                                           select r).FirstOrDefault();
                            CartItemViewModel transferObject = new CartItemViewModel
                            {
                                CartID = row.CartID,
                                ID = row.ProductID,
                                Quantity = row.Quantity,
                                IsDeleted = row.IsDeleted,
                                Name = refProd.Name,
                                Price = refProd.Price,
                                Category = refProd.Category,
                                Description = refProd.Description,
                            };

                            Model.FullPrice += refProd.Price;
                            Model.Items4Checkout.Add(transferObject);
                        }
                        Model.Order.Total = Model.FullPrice + DeliveryFee;


                    }
                }
                await InvokeAsync(StateHasChanged);
            }
            else // go to login if no user available
            {
                Nav.NavigateTo($"/login?returnto=home");
            }
        }

    }
}
