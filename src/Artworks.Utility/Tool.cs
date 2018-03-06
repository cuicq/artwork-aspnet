using Artworks.Utility.Regexs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Artworks.Utility
{
    /// <summary>
    /// 表示该类为系统工具类
    /// </summary>
    public static class Tool
    {
        #region WebRequest

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(result) || !Tool.IsIP(result))
                return "127.0.0.1";

            return result;
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, RegexPatterns.IP);
        }

        #endregion

        #region 获取hashcode

        private const int InitialPrime = 23;
        private const int FactorPrime = 29;

        /// <summary>
        /// Gets the hash code for an object based on the given array of hash
        /// codes from each property of the object.
        /// </summary>
        /// <param name="hashCodesForProperties">The array of the hash codes
        /// that are from each property of the object.</param>
        /// <returns>The hash code.</returns>
        public static int GetHashCode(params int[] hashCodesForProperties)
        {
            unchecked
            {
                int hash = InitialPrime;
                foreach (var code in hashCodesForProperties)
                    hash = hash * FactorPrime + code;
                return hash;
            }
        }


        #endregion

        #region 生成唯一int

        /// <summary>
        /// 生成唯一int
        /// </summary>
        /// <returns></returns>
        public static long GenerateID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        #endregion

        #region MD5

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string MD5(string data)
        {
            byte[] bytes = Encoding.Default.GetBytes(data);
            bytes = new MD5CryptoServiceProvider().ComputeHash(bytes);
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str = str + bytes[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }

        #endregion

        #region 字符串处理

        /// <summary>
        /// 获取数组
        /// </summary>
        /// <param name="data"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string[] GetArray(string data, char separator = ',')
        {
            if (!string.IsNullOrEmpty(data) && data.IndexOf(separator) > -1)
            {
                return data.Split(separator).ToArray();
            }
            return new string[] { data };
        }

        #endregion

        #region 获取当前请求的原始 URL

        /// <summary>
        /// 获取当前请求的原始 URL
        /// </summary>
        /// <returns></returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        #endregion

        #region 获取汉字首字母
        /// <summary>
        /// 提取汉字首字母
        /// </summary>
        /// <param name="str">需要转换的字</param>
        /// <returns>转换结果</returns>
        public static string GetChineseSpell(string str)
        {
            int len = str.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += GetSpell(str.Substring(i, 1));
            }
            return myStr;
        }

        /// <summary>
        /// 把提取的字母变成小写
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns>转换结果</returns>
        public static string GetLowerChineseSpell(string str)
        {
            return GetChineseSpell(str).ToLower();
        }

        /// <summary>
        /// 把提取的字母变成大写
        /// </summary>
        /// <param name="myChar">需要转换的字符串</param>
        /// <returns>转换结果</returns>
        public static string GetUpperChineseSpell(string str)
        {
            return GetChineseSpell(str).ToUpper();
        }
        /// <summary>
        /// 获取单个汉字的首拼音
        /// </summary>
        /// <param name="myChar">需要转换的字符</param>
        /// <returns>转换结果</returns>
        private static string GetSpell(string myChar)
        {
            byte[] arrCN = System.Text.Encoding.Default.GetBytes(myChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return System.Text.Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "_";
            }
            else return myChar;
        }

        #endregion

    }
}
