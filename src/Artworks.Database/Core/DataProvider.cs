using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Artworks.Database.Core
{
    /// <summary>
    ///  数据提供处理程序
    /// </summary>
    public abstract class DataProvider : IDataProvider
    {

        #region 创建执行命令对象

        public abstract DbConnection CreateConnection();

        /// <summary>
        /// 创建执行命令对象
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型,存储过程或SQL语句</param>
        /// <param name="parameters">SQL语句参数</param>
        /// <returns></returns>
        protected abstract DbCommand CreateCommand(string cmdText, CommandType cmdType, DbParameter[] parameters);

        #endregion

        #region 执行存储过程或SQL语句 ExecuteNonQuery

        /*

        public abstract bool ExecuteNonQuery(SqlTransaction trans, string cmdText, CommandType cmdType, params DbParameter[] parameters);

        public bool ExecuteNonQuery(SqlTransaction trans, string cmdText, CommandType cmdType)
        {
            return ExecuteNonQuery(trans, cmdText, cmdType, null);
        }

        */

        /// <summary>
        /// 执行存储过程或SQL语句 ExecuteNonQuery
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public abstract bool ExecuteNonQuery(string cmdText, CommandType cmdType, params DbParameter[] parameters);

        /// <summary>
        /// 执行存储过程或SQL语句 ExecuteNonQuery
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型</param>
        /// <returns></returns>
        public bool ExecuteNonQuery(string cmdText, CommandType cmdType)
        {
            return ExecuteNonQuery(cmdText, cmdType, null);
        }

        /// <summary>
        /// 执行存储过程或SQL语句 ExecuteNonQuery
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <returns></returns>
        public bool ExecuteNonQuery(string cmdText)
        {
            return ExecuteNonQuery(cmdText, CommandType.Text, null);
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
        public abstract DbDataReader ExecuteReader(string cmdText, CommandType cmdType, params DbParameter[] parameters);

        /// <summary>
        /// 执行存储过程或SQL语句返回DbDataReader
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型</param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(string cmdText, CommandType cmdType)
        {
            return ExecuteReader(cmdText, cmdType, null);
        }

        /// <summary>
        /// 执行存储过程或SQL语句返回DbDataReader
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(string cmdText)
        {
            return ExecuteReader(cmdText, CommandType.Text, null);
        }


        #endregion

        #region 执行存储过程或SQL语句返回第一行第一列

        /// <summary>
        /// 执行存储过程或SQL语句返回第一行第一列
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public abstract object ExecuteScalar(string cmdText, CommandType cmdType, params DbParameter[] parameters);


        /// <summary>
        /// 执行存储过程或SQL语句返回第一行第一列
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型</param>
        /// <returns></returns>
        public object ExecuteScalar(string cmdText, CommandType cmdType)
        {
            return ExecuteScalar(cmdText, cmdType, null);
        }


        /// <summary>
        /// 执行存储过程或SQL语句返回第一行第一列
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <returns></returns>
        public object ExecuteScalar(string cmdText)
        {
            return ExecuteScalar(cmdText, CommandType.Text, null);
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
        public abstract DataSet ExecuteDataSet(string cmdText, CommandType cmdType, params DbParameter[] parameters);

        /// <summary>
        ///执行存储过程或SQL语句返回DataSet
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string cmdText, CommandType cmdType)
        {
            return ExecuteDataSet(cmdText, cmdType, null);
        }

        /// <summary>
        ///执行存储过程或SQL语句返回DataSet
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string cmdText)
        {
            return ExecuteDataSet(cmdText, CommandType.Text, null);
        }



        #endregion

        #region 执行一组SQL语句并且启动事务




        #endregion

        #region 替换非法字符
        /// <summary>
        /// 替换非法字符
        /// </summary>
        /// <param name="Prams">要替换的字符</param>
        /// <returns>替换后的字符</returns>
        private object[] ReplacePrams(object[] Prams)
        {
            for (int i = 0; i < Prams.Length; i++)
            {
                if (Prams[i] is string)
                {
                    Prams[i] = Prams[i].ToString().Replace("'", "''");
                }
            }
            return Prams;
        }
        #endregion
    }
}
