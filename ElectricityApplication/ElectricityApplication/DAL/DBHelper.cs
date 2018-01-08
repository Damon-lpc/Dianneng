using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Reflection;

namespace KeyProject
{
    /// <summary>
    ///DBHelper 的摘要说明
    /// </summary>
    public class DBHelper
    {
        static string strSqlConn = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
        static string strSqlConn1 = ConfigurationManager.ConnectionStrings["sqlConnectionString1"].ConnectionString;
        public DBHelper()
        {
        }

        /// <summary>
        /// 增删改操作
        /// </summary>
        /// <param name="sql">INSERT,DELETE,UPDATE语句</param>
        /// <returns>bool是否成功</returns>
        public static int UpdateOpera(string sql, params SqlParameter[] sps)
        {
            SqlConnection conn = new SqlConnection(strSqlConn);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddRange(sps);
            conn.Open();
            int len = cmd.ExecuteNonQuery();
            conn.Close();

            return len;
        }
        /// <summary>
        /// 增删改操作
        /// </summary>
        /// <param name="sql">INSERT,DELETE,UPDATE语句</param>
        /// <returns>bool是否成功</returns>
        public static int UpdateOpera(string sql)
        {
            SqlConnection conn = new SqlConnection(strSqlConn);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            int len = cmd.ExecuteNonQuery();
            conn.Close();

            return len;
        }
        /// <summary>
        /// 得到首行首列中的数据
        /// </summary>
        /// <param name="sql">SELECT语句</param>
        /// <returns>object</returns>
        public static object GetScalar(string sql, params SqlParameter[] sps)
        {
            SqlConnection conn = new SqlConnection(strSqlConn);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddRange(sps);
            conn.Open();
            object result = cmd.ExecuteScalar();
            conn.Close();

            return result;
        }
        /// <summary>
        /// 获得数据表
        /// </summary>
        /// <param name="sql">查询SQL语句</param>
        /// <returns>DataTable</returns>
        public static List<T> GetList<T>(string sql)
        {
            List<T> t = new List<T>();
            SqlConnection conn = new SqlConnection(strSqlConn);
            SqlDataAdapter dad = new SqlDataAdapter(sql, conn);
            //dad.SelectCommand.Parameters.AddRange(sps);
            //dad.Fill(t);       //自动完成connection的开关
            return t;
        }
        /// <summary>
        /// 获得DataReader对象
        /// </summary>
        /// <param name="sql">SELECT语句</param>
        /// <returns>DataReader</returns>
        public static SqlDataReader GetReader(string sql, params SqlParameter[] sps)
        {
            SqlConnection conn = new SqlConnection(strSqlConn);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddRange(sps);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }
        public class DataTableToList<T> where T : new()
        {
            ///<summary>
            ///利用反射将Datatable转换成List模型
            ///</summary>
            ///<param name="dt"></param>
            ///<returns></returns>
            public static List<T> ConvertToList(DataTable dt)

            {

                List<T> list = new List<T>();

                Type type = typeof(T);

                string tempName = string.Empty;

                foreach (DataRow dr in dt.Rows)

                {

                    T t = new T();

                    PropertyInfo[] propertys =
                    t.GetType().GetProperties();

                    foreach (PropertyInfo pi in propertys)

                    {

                        tempName = pi.Name;

                        if (dt.Columns.Contains(tempName))

                        {

                            if (!pi.CanWrite)

                            {

                                continue;

                            }

                            var value = dr[tempName];

                            if (value != DBNull.Value)

                            {

                                pi.SetValue(t, value, null);

                            }

                        }

                    }

                    list.Add(t);

                }

                return list;

            }

        }
        ///<summary>
        ///利用反射和泛型将SqlDataReader转换成List模型
        ///</summary>
        ///<param name="sql">查询sql语句</param>
        ///<returns></returns>

        public static List<T> ExecuteToList<T>(string sql) where T : new()
        {
            SqlConnection conn = new SqlConnection(strSqlConn);            
            List<T> list;
            Type type = typeof(T);
            string tempName = string.Empty;
            SqlCommand cmd =new SqlCommand(sql, conn);
                conn.Open();
                cmd.CommandTimeout = 180;
            using (SqlDataReader reader = cmd.ExecuteReader())
                {                   
                    if (reader.HasRows)
                    {
                        list = new List<T>();
                        while (reader.Read())
                        {
                            T t = new T();
                            PropertyInfo[] propertys = t.GetType().GetProperties();
                            foreach (PropertyInfo pi in propertys)
                            {
                                tempName = pi.Name;
                                if (readerExists(reader, tempName))
                                {
                                    if (!pi.CanWrite)
                                    {
                                        continue;
                                    }
                                    var value = reader[tempName];
                                    if (value != DBNull.Value)
                                    {
                                        pi.SetValue(t, value, null);
                                    }
                                }
                            }
                            list.Add(t);
                        }
                        return list;
                    }
                }
            
            return null;
        }
        /// <summary>
        /// 判断SqlDataReader是否存在某列
        /// </summary>
        /// <param name="dr">SqlDataReader</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        private static bool readerExists(SqlDataReader dr, string columnName)
        {

            dr.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + columnName + "'";

            return (dr.GetSchemaTable().DefaultView.Count > 0);

        }
    }
}
