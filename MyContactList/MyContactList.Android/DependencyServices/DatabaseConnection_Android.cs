using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyContactList.Interfaces;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(MyContactList.Droid.DependencyServices.DatabaseConnection_Android))]
namespace MyContactList.Droid.DependencyServices
{
    public class DatabaseConnection_Android: IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "ContactsDb.db3";
            var path = Path.Combine(System.Environment.
              GetFolderPath(System.Environment.
              SpecialFolder.Personal), dbName);
            return new SQLiteConnection(path);
        }
    }
}