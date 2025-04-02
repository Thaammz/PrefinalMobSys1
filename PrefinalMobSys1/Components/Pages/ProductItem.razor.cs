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
            Model.Quantity = 1;
            Model.Status = "warning";
            Model.StatusMessage = "Add to Cart";
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

        public void AddQuantity()
        {
            Model.Quantity++;
            
        }

        public void MinusQuantity()
        {
            if (Model.Quantity > 0)
            {
                Model.Quantity--;
            }


            //removes to cart if zero quantity
            if (Model.Quantity == 0)
            {
                //UI to remove to cart
            }
        }


        public async void AddToCart() 
        {
            if (AppShell.CurrentUser != null) 
            {
                int userID = AppShell.CurrentUser.ID;
                Cart? order = null;
                List<Cart> orders = await DB.Carts();
                if (orders != null)
                {
                    order = (from r in orders
                             where r.UserID == userID
                             && !r.IsPaid
                             && !r.IsCompleted
                             select r
                         ).FirstOrDefault();
                }
                if (order == null)
                {
                    order = new Cart() {
                        UserID = AppShell.CurrentUser.ID,
                        ReferenceCode = "",
                        PaymentReference = "",
                        IsCompleted = false,
                        IsPaid = false,
                        IsDeleted = false,
                        CreatedBy = AppShell.CurrentUser.Username,
                        CreatedDate = DateTime.Now,
                    };
                    
                }
                order.ModifiedBy = AppShell.CurrentUser.Username;
                order.ModifiedDate = DateTime.Now;
                await DB.SaveCart(order);

                //check if same product exist in the cart
                List<CartItem> items = await DB.CartItems();
                var cartitm = (from r in items
                               where r.CartID == order.ID
                               && r.ProductID == Model.Item.ID
                               select r
                         ).FirstOrDefault();

                if (cartitm == null)
                {
                    cartitm = new CartItem()
                    {
                        ProductID = Model.Item.ID,
                        CartID = order.ID,
                        IsDeleted = false,
                        CreatedBy = AppShell.CurrentUser.Username,
                        CreatedDate = DateTime.Now,
                    };
                }

                cartitm.Quantity = Model.Quantity;
                cartitm.ModifiedBy = AppShell.CurrentUser.Username;
                cartitm.ModifiedDate = DateTime.Now;

                await DB.SaveCartItem(cartitm);
                Model.Status = "danger";
                Model.StatusMessage = "Remove from Cart";
            }
            else // go to login if no user available
            {
                Nav.NavigateTo($"/login?returnto=productitem-{productid}");
            }
            await InvokeAsync(StateHasChanged);//refresh rendered page
        } 
    }
}
