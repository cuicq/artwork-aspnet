using Artworks.Configuration.Initialize.Internal;

namespace Artworks.Configuration.Initialize
{

    /// <summary>
    /// 配置初始化容器管理。
    /// </summary>
    public class ConfigurationInitializerContainer
    {
        private static readonly object lockObj = new object();
        private static IConfigurationInitializer instanceObj;

        /// <summary>
        /// 实例。
        /// </summary>
        public static IConfigurationInitializer Instance
        {
            get
            {
                if (instanceObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceObj == null)
                        {
                            instanceObj = new ConfigurationCeInitializer();
                        }
                    }
                }
                return instanceObj;
            }
        }

    }
}
