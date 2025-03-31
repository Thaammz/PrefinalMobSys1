using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class ProductsViewModel: BaseViewModel
    {
        public string Search { get; set; }
        public string LoadedPhotoPath { get; set; }
        public Product SelectedProduct { get; set; } = new Product();
        public List<Product> Products { get; set; } = new List<Product>();
    }

    //public class ProductItemModel
    //{
    //    public int ID { get; set; }
    //    public string Name { get; set; }
    //    public string Category { get; set; }
    //    public string Description { get; set; }
    //    public double Price { get; set; }
    //    public string SKU { get; set; }
    //    public bool IsActive { get; set; }
    //    public bool IsDeleted { get; set; }
    //    public string CreatedBy { get; set; }
    //    public string ModifiedBy { get; set; }
    //    public DateTime CreatedDate { get; set; }
    //    public DateTime ModifiedDate { get; set; }
    //    public string Icon { get; set; }
    //}
}
