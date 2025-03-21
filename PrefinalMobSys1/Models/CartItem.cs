using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class CartItem
    {
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public int CartID { get; set; }

        [NotNull]
        public int ProductID { get; set; }

        [NotNull]
        public int Quantity { get; set; }

        [NotNull]
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
