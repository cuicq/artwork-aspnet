using Artworks.Infrastructure.Application.Configuration;

namespace Artworks.Infrastructure.Application.Web.Security.Internal
{
    /// <summary>
    /// 配置帮助。
    ///</summary>
    internal class ConfigUtil
    {
        public static string CookieName { get { return ApplicationConfiguration.Instance.GetValue<string>("cookie"); } }//cookie
        public static string DomainName { get { return ApplicationConfiguration.Instance.GetValue<string>("domain"); } }//域
        public static string SecretKey { get { return ApplicationConfiguration.Instance.GetValue<string>("secretkey"); } }//秘钥
    }

    /// <summary>
    /// 系统常量。
    /// </summary>
    internal class Consts
    {
        /// <summary>
        /// 分隔符
        /// </summary>
        public const char separator = '@';
    }
}
