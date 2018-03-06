using Artworks.Database.CommonModel;

namespace Artworks.Database.Initialize
{
    /// <summary>
    /// 数据库初始化接口。
    /// </summary>
    public interface IDatabaseInitializer
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        void InitializeDatabase();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseConnectionStringManager"></param>
        void SetInitializeDatabase(DatabaseConnectionStringManager databaseConnectionStringManager);
        /// <summary>
        /// 获取链接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetConnectionString(string key);

    }
}
