using System;
using System.IO;
using SQLite;

namespace BIM494_Assigment_IV
{
    public static class MyConnectionFactory
    {
        static SQLiteConnection connection;
        public static SQLiteConnection Instance
        {
            get
            {
                return connection ??(connection = CreateConnection());
            }
        }

        private static SQLiteConnection CreateConnection()
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MyData.db");
                var connection = new SQLiteConnection(databasePath);
                return connection;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}