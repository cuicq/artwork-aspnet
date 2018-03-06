using Artworks.Anomaly;
using Artworks.Database.CommonModel;
using Artworks.Database.CommonModel.Internal;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Artworks.Database.Core.SqlServer
{
    /// <summary>
    /// 轻量级SQLSERVER数据库操作类。
    /// </summary>
    public class SqlServerDataProvider : DataProvider
    {

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlServerDataProvider()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">链接字符串</param>
        public SqlServerDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #endregion

        #region 变量

        /// <summary>
        /// 其他链接字符串
        /// </summary>
        private string connectionString = string.Empty;
        /// <summary>
        /// 链接字符串缓存
        /// </summary>
        private static Hashtable hashtable = Hashtable.Synchronized(new Hashtable());

        #endregion

        #region 创建执行命令对象

        public override DbConnection CreateConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }

        /// <summary>
        /// 创建执行命令对象
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型,存储过程或SQL语句</param>
        /// <param name="parameters">SQL语句参数</param>
        /// <returns></returns>
        protected override DbCommand CreateCommand(string cmdText, CommandType cmdType, DbParameter[] parameters)
        {
            DbConnection con = new SqlConnection(connectionString);
            DbCommand cmd = con.CreateCommand();
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 120;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            return cmd;
        }

        #endregion

        #region 执行存储过程或SQL语句 ExecuteNonQuery

        /*
        public override bool ExecuteNonQuery(SqlTransaction trans, string cmdText, CommandType cmdType, params DbParameter[] parameters)
        {

            SqlCommand cmd = new SqlCommand();
            var conn = trans.Connection;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val > 0 ? true : false;
        }
        */

        /// <summary>
        /// 执行存储过程或SQL语句 ExecuteNonQuery
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public override bool ExecuteNonQuery(string cmdText, CommandType cmdType, params DbParameter[] parameters)
        {
            DbCommand cmd = CreateCommand(cmdText, cmdType, parameters);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex)
            {
                ExceptionHelper.HandleException(cmdText, ex);
                return false;
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Parameters.Clear();
            }
        }

        #endregion

        #region 执行存储过程或SQL语句返回DbDataReader

        /// <summary>
        /// 执行存储过程或SQL语句返回DbDataReader
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public override DbDataReader ExecuteReader(string cmdText, CommandType cmdType, params DbParameter[] parameters)
        {
            DbCommand cmd = CreateCommand(cmdText, cmdType, parameters);
            try
            {
                cmd.Connection.Open();
                return (DbDataReader)cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (System.Exception ex)
            {
                ExceptionHelper.HandleException(cmdText, ex);

                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
                throw;
            }
            finally
            {
                cmd.Parameters.Clear();
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
            }
        }
        #endregion

        #region 执行存储过程或SQL语句返回第一行第一列

        /// <summary>
        /// 执行存储过程或SQL语句 ExecuteScalar 返回第一行第一列
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public override object ExecuteScalar(string cmdText, CommandType cmdType, params DbParameter[] parameters)
        {
            DbCommand cmd = CreateCommand(cmdText, cmdType, parameters);
            try
            {
                cmd.Connection.Open();
                return cmd.ExecuteScalar();
            }
            catch (System.Exception ex)
            {
                ExceptionHelper.HandleException(cmdText, ex);
                throw;
            }
            finally
            {
                cmd.Parameters.Clear();
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
            }
        }

        #endregion

        #region 执行存储过程或SQL语句返回DataSet


        /// <summary>
        ///执行存储过程或SQL语句返回DataSet
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public override DataSet ExecuteDataSet(string cmdText, CommandType cmdType, params DbParameter[] parameters)
        {
            DbDataAdapter dap = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                dap.SelectCommand = CreateCommand(cmdText, cmdType, parameters);
                dap.Fill(ds);
            }
            catch (System.Exception ex)
            {
                ExceptionHelper.HandleException(cmdText, ex);
                throw;
            }
            finally
            {
                if (dap.SelectCommand.Connection.State == ConnectionState.Open)
                    dap.SelectCommand.Connection.Close();
                if (CommandType.Text != cmdType)
                    dap.SelectCommand.Parameters.Clear();
                dap.Dispose();
            }
            return ds;
        }

        #endregion

    }
}
