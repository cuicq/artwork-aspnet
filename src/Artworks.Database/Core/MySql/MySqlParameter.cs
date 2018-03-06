using System.Data;
using MySqlPlug = MySql.Data;

namespace Artworks.Database.Core.MySql
{
    /// <summary>
    /// MySql 数据库参数。
    /// </summary>
    internal class MySqlParameter : DataParameter
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
            MySqlPlug.MySqlClient.MySqlParameter parameter = new MySqlPlug.MySqlClient.MySqlParameter();
            parameter.ParameterName = name;
            parameter.MySqlDbType = (MySqlPlug.MySqlClient.MySqlDbType)type;
            if (size == 16
                || size == -1
                )
            {
                if (value is byte[])
                {
                    parameter.Size = ((byte[])value).Length;
                }
                else
                {
                    parameter.Size = value.ToString().Length;
                }
            }
            else
            {
                parameter.Size = size;
            }
            parameter.Direction = direction;
            if (value != null && direction == ParameterDirection.Input)
            {
                parameter.Value = value;
            }
            return parameter;
        }

    }

}
