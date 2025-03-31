using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class UsersViewModel: BaseViewModel
    {
        /// <summary>
        /// To Identify if the form is new or an update
        /// </summary>
        public bool SelectMode { get; set; } = false;
        public User SelectedUser { get; set; } = new User();
        public List<User> Users { get; set; } = new List<User>();
    }
}
