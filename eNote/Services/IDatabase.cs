using System;
using SQLite;

namespace eNote
{
    public interface IDatabase
    {
        SQLiteConnection GetConnection();
    }
}
