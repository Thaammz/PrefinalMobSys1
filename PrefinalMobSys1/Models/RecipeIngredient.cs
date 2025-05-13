using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class RecipeIngredient
    {
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public int RecipeID { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public double Amount { get; set; }

        [NotNull]
        public string Unit { get; set; }

        [NotNull]
        public int CalorieCount { get; set; }

        public string Note { get; set; }

        [NotNull]
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
