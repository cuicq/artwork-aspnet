using Artworks.Utility.Encryption;

namespace Artworks.Infrastructure.Application.Web.Security.Internal
{
    /// <summary>
    /// 密码帮助类。
    /// </summary>
    internal class Cyptography
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">字符串</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key = "artworks")
        {
            return DESEncrypt.Encrypt(data, key);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">加密的数据</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string Decrypt(string data, string key = "artworks")
        {
            string txt = DESEncrypt.Decrypt(data, key);

            if (data == txt)
            {
                return string.Empty;
            }

            return txt;

        }


    }
}
