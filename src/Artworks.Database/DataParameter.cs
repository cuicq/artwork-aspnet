using System.Data;
using System.Data.Common;
namespace Artworks.Database
{
    /// <summary>
    /// 数据库参数抽象类。
    /// </summary>
    public abstract class DataParameter
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
        protected abstract DbParameter MakeParameter(
            string name,
            DbType type,
            int size,
            object value,
            ParameterDirection direction);

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">数据类型</param>
        /// <param name="size">数据大小</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public DbParameter MakeInParameter(
            string name,
            DbType type,
            int size,
            object value
            )
        {
            return MakeParameter(name, type, size, value, ParameterDirection.Input);
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">数据类型</param>
        /// <param name="size">数据大小</param>
        /// <returns></returns>
        public DbParameter MakeOutParameter(
            string name,
            DbType type,
            int size
            )
        {
            return MakeParameter(name, type, size, null, ParameterDirection.Output);
        }

    }

}
