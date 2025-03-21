using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class BaseViewModel
    {
        /// <summary>
        /// To Identify if the form is new or an update
        /// </summary>
        public bool IsNew { get; set; } = false;

        /// <summary>
        /// Control to show or hide the edit form of a page
        /// </summary>
        public bool ShowForm { get; set; } = false;

        /// <summary>
        /// Control to change the type of Bootsrap "alert", it can be success, danger or warning
        /// </summary>
        public string Status { get; set; } = "success";

        /// <summary>
        /// Store the message for the alert
        /// </summary>
        public string StatusMessage { get; set; } = "";
    }
}
