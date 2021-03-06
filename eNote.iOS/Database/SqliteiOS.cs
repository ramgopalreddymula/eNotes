﻿using System;
using System.IO;
using eNote;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SqliteiOS))]
namespace eNote
{
    public class SqliteiOS : IDatabase
    {
        public SQLiteConnection GetConnection()
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "eNote.db");
                var sqlConn = new SQLiteConnection(dbPath);
                return sqlConn;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
