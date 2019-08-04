using System;
using System.IO;
using eNote;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SqliteAndroid))]
namespace eNote
{
    public class SqliteAndroid : IDatabase
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
        //public void GetFile()
        //{
        //    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //    string destinationPath = Path.Combine(folderPath, "eNote.db");
        //    // Check if it exists at that path
        //    if (File.Exists(destinationPath))
        //    {
        //        // Copy the file to the app's local folder
               
        //        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "eNote.db");
        //        //File.Copy(filePath, destinationPath);
        //        File.WriteAllText(dbPath, destinationPath);
        //    }
        //    else
        //    {

        //    }
        //}
        //public void CopyDBToSdCard(string dbName)
        //{
        //    File.Copy(GetDBPath(dbName), Path.Combine(Android.OS.Environment.DirectoryDownloads, dbName));
        //}
        //public string GetDBPath(string dbFileName)
        //{
        //    string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "eNote.db");

        //    if (!Directory.Exists(dbPath))
        //        Directory.CreateDirectory(dbPath);
        //    return context.GetDatabasePath(dbFileName).AbsolutePath;
        //}
    }
}
