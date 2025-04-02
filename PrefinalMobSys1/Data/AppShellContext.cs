using Newtonsoft.Json;
using PrefinalMobSys1.Models;
using PrefinalMobSys1.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Data
{
    /// <summary>
    /// Centralized Class for handing global level things for the App
    /// Loaded in MauiProgram as Singleton (one instance only within the App)
    /// </summary>
    public class AppShellContext
    {
        public static AppShellContext Instance { set; get; }

        public AppShellContext()
        {
            Instance = this;
        }

        private bool _isUserLoggedIn;
        public bool IsUserLoggedIn
        {
            get
            {
                return _isUserLoggedIn;
            }
            set
            {
                if (_isUserLoggedIn != value)
                {
                    _isUserLoggedIn = value;
                    NotifyStateChanged();
                }
            }
        }
        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public User CurrentUser { get; set; }

        public void SetSessionUser(User CurrentUser)
        {
            var uFilePath = FileSystem.AppDataDirectory + "/sid.key";
            var jsonRaw = JsonConvert.SerializeObject(CurrentUser);
            var encodedData = StringUtilities.Base64Encode(jsonRaw);
            File.WriteAllText(uFilePath, encodedData);
        }

        public User GetSessionUser()
        {
            User res = null;
            var uFilePath = FileSystem.AppDataDirectory + "/sid.key";

            if (File.Exists(uFilePath))
            {
                try
                {
                    var encodedData = File.ReadAllText(uFilePath);
                    var jsonRaw = StringUtilities.Base64Decode(encodedData);
                    res = JsonConvert.DeserializeObject<User>(jsonRaw);
                }
                catch (Exception ex) { }
            }
            return res;
        }

        public void ClearSessionUser()
        {
            var uFilePath = FileSystem.AppDataDirectory + "/sid.key";
            File.Delete(uFilePath);
        }
    }
}
