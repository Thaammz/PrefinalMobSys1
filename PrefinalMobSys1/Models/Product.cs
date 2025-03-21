using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class Product
    {
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Category { get; set; }

        public string Description { get; set; }
        
        [NotNull]
        public double Price { get; set; }
        
        [NotNull]
        public string SKU { get; set; }

        [NotNull]
        public bool IsActive { get; set; }

        [NotNull]
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
