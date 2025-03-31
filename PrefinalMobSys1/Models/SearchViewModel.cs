using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class SearchViewModel : BaseViewModel
    {
        public string Search { get; set; }
        public string Category { get; set; }
        public List<Product> Result { get; set; } = new List<Product>();        
    }
}
