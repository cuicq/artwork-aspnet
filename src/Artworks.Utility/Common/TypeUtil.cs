using System;
using System.Text.RegularExpressions;

namespace Artworks.Utility.Common
{
    /// <summary>
    /// 类型帮助类。
    /// </summary>
    public class TypeUtil
    {

        /// <summary>
        /// 获取Int类型
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="result">返回值,默认为0</param>
        /// <returns></returns>

        public static int GetInt(object obj, int result)
        {
            return TypeUtil.GetInt(obj, out result);
        }

        /// <summary>
        /// 获取Int类型
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="result">返回值,默认为0</param>
        /// <returns></returns>
        public static int GetInt(object obj, out int result)
        {
            result = 0;
            if (obj == null) return result;
            var temp = obj.ToString();

            if (!string.IsNullOrEmpty(temp))
            {
                int.TryParse(temp, out result);
            }

            return result;
        }

        public static decimal GetDecimal(string s, out decimal result)
        {
            result = 0.00M;
            if (!string.IsNullOrEmpty(s))
            {
                decimal.TryParse(s, out result);
            }
            return result;
        }

        /// <summary>
        /// 获取时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string str)
        {
            DateTime result = DateTime.MinValue;
            if (!string.IsNullOrEmpty(str))
            {
                DateTime.TryParse(str, out result);
            }
            return result;
        }

        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return true;
                    }
                }
            }
            return false;
        }



    }
}
