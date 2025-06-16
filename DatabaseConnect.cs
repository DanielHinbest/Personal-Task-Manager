using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Task_Manager
{
    internal abstract class DatabaseConnect
    {
        public static SqliteConnection Connect()
        {
            SqliteConnection connection = new SqliteConnection("Data Source=tasks.db;Version=3;");
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return connection;
        }
    }
}
