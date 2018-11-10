using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using MyContactList.Interfaces;
using SQLite;
using UIKit;
[assembly: Xamarin.Forms.Dependency(typeof(MyContactList.iOS.DependencyServices.DatabaseConnection_iOS))]
namespace MyContactList.iOS.DependencyServices
{
    public class DatabaseConnection_iOS : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "ContactsDb.db3";
            string personalFolder =
              System.Environment.
              GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder =
              Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryFolder, dbName);
            return new SQLiteConnection(path);
        }
    }
}