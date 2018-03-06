using System.Data;
using System.Data.Common;

namespace Artworks.Database
{
    /// <summary>
    /// 数据处理提供程序接口。
    /// </summary>
    public interface IDataProvider
    {

        /// <summary>
        /// 执行存储过程或SQL语句 ExecuteNonQuery
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        bool ExecuteNonQuery(string cmdText, CommandType cmdType, params DbParameter[] parameters);

        /// <summary>
        /// 执行存储过程或SQL语句返回DbDataReader
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        DbDataReader ExecuteReader(string cmdText, CommandType cmdType, params DbParameter[] parameters);

        /// <summary>
        /// 执行存储过程或SQL语句返回第一行第一列
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        object ExecuteScalar(string cmdText, CommandType cmdType, params DbParameter[] parameters);


        /// <summary>
        ///执行存储过程或SQL语句返回DataSet
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句或存储过程名称</param>
        /// <param name="cmdType">语句类型</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        DataSet ExecuteDataSet(string cmdText, CommandType cmdType, params DbParameter[] parameters);

    }

}
