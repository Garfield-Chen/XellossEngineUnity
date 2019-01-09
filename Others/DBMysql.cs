using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace tyoEngineEditor
{

    public class DBMysql
    {
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn,
        MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        public static DataSet GetDataSet(string cmdText)
        {
            //创建一个MySqlCommand对象
            MySqlCommand cmd = new MySqlCommand();
            //创建一个MySqlConnection对象
            MySqlConnection conn = new MySqlConnection(Parameters.connectionStr);

            try
            {
                CommandType cmdType = CommandType.Text;
                //调用 PrepareCommand 方法，对 MySqlCommand 对象设置参数
                PrepareCommand(cmd, conn, null, cmdType, cmdText, null);
                //调用 MySqlCommand  的 ExecuteReader 方法
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();

                adapter.Fill(ds);
                //清除参数
                cmd.Parameters.Clear();
                conn.Close();
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet GetDataSet(MySqlCommand cmd, MySqlConnection conn, MySqlParameter[] cmdParms)
        {
            try
            {
                CommandType cmdType = CommandType.Text;
                //调用 PrepareCommand 方法，对 MySqlCommand 对象设置参数
                PrepareCommand(cmd, conn, null, cmdType, cmd.CommandText, cmdParms);
                //调用 MySqlCommand  的 ExecuteReader 方法
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();

                adapter.Fill(ds);
                //清除参数
                cmd.Parameters.Clear();
                conn.Close();
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void ExecuteSqlTran(List<string> SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(Parameters.connectionStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                        //后来加上的  
                        if (n > 0 && (n % 500 == 0 || n == SQLStringList.Count - 1))
                        {
                            tx.Commit();
                            tx = conn.BeginTransaction();
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
            }
        }

        public static int ExecuteNonQuery(MySqlCommand cmd, params MySqlParameter[] commandParameters)
        {
            foreach (MySqlParameter parm in commandParameters)
                cmd.Parameters.Add(parm);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 用指定的数据库连接字符串执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <remarks>
        ///例如:
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        ///<param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public static object ExecuteScalar(string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();

            using (MySqlConnection connection = new MySqlConnection(Parameters.connectionStr))
            {
                CommandType cmdType = CommandType.Text;
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
    }
}
