using System;
using System.Security.Cryptography;

namespace Artworks.Utility.Encryption
{
    /// <summary>
    /// 哈希工具。
    /// </summary>
    public static class HashUtil
    {
        /// <summary>
        /// 随机生成一个盐度。
        /// </summary>
        public static byte[] GenerateSalt(int size)
        {
            var salt = new byte[size];
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                cryptoProvider.GetNonZeroBytes(salt);
            }

            return salt;
        }

        /// <summary>
        /// 计算指定字节的哈希。
        /// </summary>
        public static byte[] Hash(byte[] bytes, string hashName = "SHA1")
        {
            if (bytes == null)
            {
                return null;
            }

            using (var algorithm = HashAlgorithm.Create(hashName))
            {
                return algorithm.ComputeHash(bytes);
            }
        }

        /// <summary>
        /// 计算指定字符串的哈希。
        /// </summary>
        public static string Hash(string str, string hashName = "SHA1", string encodingName = "UTF-8")
        {
            if (string.IsNullOrEmpty(str))
            {
                return String.Empty;
            }

            var bytes = System.Text.Encoding.Default.GetBytes(str);
            var hashedBytes = Hash(bytes, hashName);

            // var hashedBytes = Hash(str.ToBytes(encodingName), hashName);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }

        /// <summary>
        /// 使用 MD5 算法计算指定字符串的哈希。
        /// </summary>
        public static string MD5(string str, string salt = "ARTWORKS", string encodingName = "UTF-8")
        {
            return Hash(salt + str, "MD5");
        }
    }
}
