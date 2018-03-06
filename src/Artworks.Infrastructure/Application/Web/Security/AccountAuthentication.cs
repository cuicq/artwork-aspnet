using Artworks.Infrastructure.Application.Web.Security.Internal;
using Artworks.Utility.Common;
using System.Collections.Generic;

namespace Artworks.Infrastructure.Application.Web.Security
{

    /// <summary>
    /// 为 Web 应用程序管理账户身份验证服务。此类不能被继承。
    /// </summary>
    public sealed class AccountAuthentication
    {

        /// <summary>
        /// 域
        /// </summary>
        public static string Domain { get { return ConfigUtil.DomainName; } }

        /// <summary>
        /// 注册客户端数据。
        /// </summary>
        /// <param name="displayName">显示名</param>
        /// <param name="extraData">扩展数据</param>
        /// <param name="persistCookie">是否保留cookie，保留时间24h*30</param>
        public static void RegisterClient(string displayName, IDictionary<string, string> extraData, bool persistCookie = false)
        {
            AuthenticationClientData clientData = new AuthenticationClientData(displayName, extraData);
            int hours = persistCookie == true ? 24 * 30 : 0;
            CookieUtil.SetCookie(ConfigUtil.CookieName, Cyptography.Encrypt(clientData.ToString(), ConfigUtil.SecretKey), hours, ConfigUtil.DomainName);
        }


        /// <summary>
        /// 获取认证客户端信息
        /// </summary>
        public static AuthenticationClientData RegisteredClientData
        {
            get
            {
                AuthenticationClientData clientData = null;

                string data = string.Empty;
                string userData = CookieUtil.GetCookie(ConfigUtil.CookieName);
                if (!string.IsNullOrEmpty(userData))
                {
                    data = Cyptography.Decrypt(userData, ConfigUtil.SecretKey);
                    if (!string.IsNullOrEmpty(data))
                    {

                        IDictionary<string, string> extraData = new Dictionary<string, string>();

                        string[] array = data.Split(Consts.separator);
                        foreach (var str in array)
                        {
                            if (!string.IsNullOrEmpty(str))
                            {

                                string[] list = str.Split(':');
                                string key = list[0];
                                string value = list[1];
                                if (!extraData.ContainsKey(key))
                                {
                                    extraData.Add(key, value);
                                }
                            }
                        }

                        string displayName = string.Empty;
                        extraData.TryGetValue("name", out displayName);

                        clientData = new AuthenticationClientData(displayName, extraData);

                    }
                }

                return clientData;
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        public static void Logout()
        {
            CookieUtil.ClearCookie(ConfigUtil.CookieName, ConfigUtil.DomainName);
        }
    }
}
