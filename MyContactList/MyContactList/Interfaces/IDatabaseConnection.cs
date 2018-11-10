using System;
using System.Collections.Generic;
using System.Text;

namespace MyContactList.Interfaces
{
    public interface IDatabaseConnection
    {
        SQLite.SQLiteConnection DbConnection();

    }
}
