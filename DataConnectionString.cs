using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Database1
{
    internal class DataConnectionString
    {
        static String filename = "Database.accdb";
        static String MainPath = AppDomain.CurrentDomain.BaseDirectory;

        public static String connectionString;
        public DataConnectionString()
        {
            String Connection = MainPath.Replace("\\bin\\Debug", "");
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Connection + filename;
        }
    }
}
