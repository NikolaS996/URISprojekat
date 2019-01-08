using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URIS2018UtilDemo.DataAccess
{
    //Staticka klasa koja sadrzi sve zajednicke funkcije mikroservisa
    public static class DBFunctions
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ADOConnection"].ConnectionString;
            }
        }

        public static void AddParameter(this SqlCommand cmdToFill, string paramName, SqlDbType paramType, object param)
        {
            cmdToFill.Parameters.Add(paramName, paramType);
            if (param != null)
                cmdToFill.Parameters[paramName].Value = param;
            else
                cmdToFill.Parameters[paramName].Value = DBNull.Value;
        }
    }
}
