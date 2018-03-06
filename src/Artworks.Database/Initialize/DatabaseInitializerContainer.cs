using Artworks.Database.Initialize.Internal;

namespace Artworks.Database.Initialize
{
    /// <summary>
    /// 数据库初始化容器
    /// </summary>
    public class DatabaseInitializerContainer
    {

        private static readonly object sync = new object();
        private static IDatabaseInitializer databaseInitializer;

        public static IDatabaseInitializer Instance
        {
            get
            {
                if (databaseInitializer == null)
                {
                    lock (sync)
                    {
                        if (databaseInitializer == null)
                        {
                            databaseInitializer = new SqlCeInitializer();
                        }
                    }
                }
                return databaseInitializer;
            }
        }
    }
}
