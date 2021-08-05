using System;
using System.Data.SQLite;

namespace RubyNet.Database.Data
{
    public class SqLiteBaseRepository
    {
        protected static string DbFile => Environment.CurrentDirectory + "\\RubyDb.sqlite";

        protected static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }
    }
}