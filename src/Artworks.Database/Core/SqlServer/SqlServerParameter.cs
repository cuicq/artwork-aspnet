using System.Data;
using System.Data.SqlClient;

namespace Artworks.Database.Core.SqlServer
{
    /// <summary>
    /// Sql Server 数据库参数。
    /// </summary>
    internal class SqlServerParameter : DataParameter
    {
        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">数据类型</param>
        /// <param name="size">数据大小</param>
        /// <param name="value">参数值</param>
        /// <param name="direction">参类型</param>
        /// <returns></returns>
        protected override System.Data.Common.DbParameter MakeParameter(string name, System.Data.DbType type, int size, object value, System.Data.ParameterDirection direction)
        {

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = name;
            sqlParameter.SqlDbType = (SqlDbType)type;
            if (size == 16
                || size == -1
                )
            {
                if (value is byte[])
                {
                    sqlParameter.Size = ((byte[])value).Length;
                }
                else
                {
                    sqlParameter.Size = value.ToString().Length;
                }
            }
            else
            {
                sqlParameter.Size = size;
            }
            sqlParameter.Direction = direction;
            if (value != null && direction == ParameterDirection.Input)
            {
                sqlParameter.Value = value;
            }
            return sqlParameter;
        }

    }

}
