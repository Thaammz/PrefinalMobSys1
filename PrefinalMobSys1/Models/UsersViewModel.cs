using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class UsersViewModel: BaseViewModel
    {        
        public User SelectedUser { get; set; } = new User();
        public List<User> Users { get; set; } = new List<User>();
    }
}
