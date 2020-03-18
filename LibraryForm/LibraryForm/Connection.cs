using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LibraryForm
{
    class Connection
    {
        public static SqlConnection conn = null;

        public static string ConnectionString = "Data Source=LAPTOP-V3CVRCC9\\SQLEXPRESS;Initial Catalog=bsulib;Integrated Security=True";

        public static void OpenConnection()
        {
            conn = new SqlConnection(ConnectionString);
        }

        public static void CloseConnection()
        {
            conn.Close();
        }
    }
}
