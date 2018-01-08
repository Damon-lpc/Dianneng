using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ElectricityApplication.DAL
{
    public class DapperHelper
    {
        static string sqlconnection = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;   
        public static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);  //这里sqlconnection就是数据库连接字符串
            connection.Open();
            return connection;
        }
        public IEnumerable<T> SelectColumn<T>(string query)
        {        
            using (IDbConnection conn = DapperHelper.OpenConnection())
            {               
                return conn.Query<T>(query, null); 
            }
        }
    }
}