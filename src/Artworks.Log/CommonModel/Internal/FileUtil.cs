using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Artworks.Log.CommonModel.Internal
{
    /// <summary>
    /// 文件工具。
    /// </summary>
    public class FileUtil
    {
        private static Mutex mutex = new Mutex();//防止多个线程写一个文件

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="message"></param>
        public static void Write(string file, string message, string encoding = "GB2312")
        {
            string target = FormatFileName(file);
            DirectoryUtil.CreateDirectoryIfNotExists(Path.GetDirectoryName(target));
            Thread thread = new Thread(new ParameterizedThreadStart(FileUtil.Execute));
            thread.Start(new object[] { target, message, encoding });
        }

        private static void Execute(object data)
        {
            var arguments = (object[])data;

            var file = arguments[0].ToString();
            var message = arguments[1];
            string encoding = arguments[2].ToString();

            string format = @"------" + DateTime.Now.ToString() + @"  " + message;

            mutex.WaitOne();

            using (StreamWriter sw = new StreamWriter(file, true, System.Text.Encoding.GetEncoding(encoding)))
            {
                sw.WriteLine(format);
                sw.Close();
            }

            mutex.ReleaseMutex();
        }


        /// <summary>
        /// 文件名格式化
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string FormatFileName(string input)
        {
            Regex regex = new Regex("{(?<name>(?:.+?))}");
            var ms = regex.Matches(input);
            string temp = input;
            if (ms.Count > 0)
            {
                var date = DateTime.Now;

                for (var i = 0; i < ms.Count; i++)
                {
                    var m = ms[i];
                    if (m.Success)
                    {
                        string value = m.Groups["name"].Value;
                        temp = temp.Replace("{" + value + "}", date.ToString(value));
                    }
                }
            }
            return temp;
        }
    }
}
