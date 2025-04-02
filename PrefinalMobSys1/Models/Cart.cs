using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class Cart
    {
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public int UserID { get; set; }

        [NotNull]
        public double Total { get; set; }

        [NotNull]
        public string ReferenceCode { get; set; }

        [NotNull]
        public string PaymentReference { get; set; }

        [NotNull]
        public bool IsCompleted { get; set; }

        [NotNull]
        public bool IsPaid { get; set; }

        [NotNull]
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
