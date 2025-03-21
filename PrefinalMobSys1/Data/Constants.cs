using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Data
{
    public static class Constants
    {
        /// <summary>
        /// Name for the SQLite DB created in the device
        /// </summary>
        public const string DatabaseFilename = "applocal.db";


        /// <summary>
        /// Flags or Settings on what the SQLite DB can do
        /// </summary>
        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;


        /// <summary>
        /// Property that returns the Database Location Path
        /// </summary>
        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }
}
