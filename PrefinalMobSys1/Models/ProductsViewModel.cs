using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class ProductsViewModel: BaseViewModel
    {        
        public Product SelectedProduct { get; set; } = new Product();
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
