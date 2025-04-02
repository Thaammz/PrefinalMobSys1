using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class CartViewModel : BaseViewModel
    {
        public double FullPrice { get; set; }
        public Cart Order { get; set; }
        public List<CartItemViewModel> Items4Checkout { get; set; } = new List<CartItemViewModel>();        
    }

    public class CartItemViewModel:Product
    {
        public int Quantity { get; set; }

        public int CartID { get; set; }

        //public int ProductID { get; set; }
        //public string ProductName { get; set; }
        //public string ProductCategory { get; set; }
        //public string ProductDescription { get; set; }
        //public double ProductPrice { get; set; }
        //public string ProductSKU { get; set; }
        //public bool ProductIsActive { get; set; }

        //public bool IsDeleted { get; set; }
        //public string CreatedBy { get; set; }
        //public string ModifiedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime ModifiedDate { get; set; }
    }
}
