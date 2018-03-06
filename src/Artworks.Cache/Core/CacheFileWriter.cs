using Artworks.Cache.CommonModel.Internal;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;

namespace Artworks.Cache.Core
{
    /// <summary>
    /// 表示该类为缓存文件写入类
    /// </summary>
    public class CacheFileWriter
    {
        private static object lockObj = new object();

        /// <summary>
        /// 同步更新文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="safety"></param>
        private static void Synchronization(string path, bool safety = false)
        {
            if (safety)
            {
                lock (lockObj)
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        FileStream fs = null;
                        try
                        {
                            FileInfo info = new FileInfo(path);
                            fs = info.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            info.CopyTo(path.Replace("_temp.", "."), true);
                            fs.Close();
                            info.Delete();
                        }
                        finally
                        {
                            if (fs != null)
                            {
                                fs.Close();
                            }
                        }
                    }
                }
            }
            else
            {
                Synchronization(path);
            }
        }

        /// <summary>
        /// 同步更新文件
        /// </summary>
        /// <param name="file"></param>
        public static void Synchronization(string file)
        {
            try
            {
                //可以同步到其他文件夹目录下
                if (!string.IsNullOrEmpty(file))
                {
                    new System.Xml.XmlDocument().Load(file);
                    FileInfo info = new FileInfo(file);
                    info.CopyTo(file.Replace("_temp.", "."), true);
                    info.Delete();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.HandleException(ex);           
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dt"></param>
        public static void Write(string file, DataTable dt, bool safety = false)
        {
            try
            {
                //生成临时文件存储
                string extension = Path.GetExtension(file);
                string tempfile = string.Format("{0}_temp{1}", file.Substring(0, file.IndexOf(extension)), extension);

                DirectoryUtil.CreateDirectoryIfNotExists(Path.GetDirectoryName(tempfile));

                StringBuilder xml = new StringBuilder();
                xml.Append("<?xml version=\"1.0\" encoding=\"GB2312\"?>");
                xml.Append("<data>");

                StringBuilder types = new StringBuilder();
                StringBuilder names = new StringBuilder();
                //column
                var columnlength = dt.Columns.Count;
                for (var i = 0; i < columnlength; i++)
                {
                    names.AppendFormat("{0}|", dt.Columns[i].ColumnName);
                    types.AppendFormat("{0}|", dt.Columns[i].DataType);
                }

                xml.AppendFormat(names.ToString().TrimEnd('|'));
                xml.Append(",");
                xml.AppendFormat(types.ToString().TrimEnd('|'));
                xml.Append(",");

                StringBuilder dataItem = null;

                foreach (DataRow row in dt.Rows)
                {
                    dataItem = new StringBuilder();

                    for (var i = 0; i < columnlength; i++)
                    {
                        if (row[i] != null && !string.IsNullOrEmpty(row[i].ToString()))
                        {
                            string text = row[i].ToString();
                            dataItem.Append(HttpUtility.HtmlEncode(text.Replace(",", "###").Replace("|", "@@@") + "|"));
                        }
                        else
                        {
                            dataItem.Append("|");
                        }
                    }
                    xml.Append(dataItem.ToString().TrimEnd('|'));
                    xml.Append(",");
                }

                string content = xml.ToString().TrimEnd(',') + "</data>";

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);

                Write(tempfile, content);
                Synchronization(tempfile, safety);

            }
            catch (System.Exception ex)
            {
                ExceptionHelper.HandleException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        private static void Write(string filePath, string content)
        {
            lock (lockObj)
            {
                FileStream stream = null;
                try
                {
                    using (stream = File.Open(filePath, FileMode.Create))
                    {
                        byte[] bytes = System.Text.Encoding.Default.GetBytes(content);
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (System.Exception ex)
                {
                    ExceptionHelper.HandleException(ex);
                    throw ex;
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Dispose();
                        stream.Close();
                    }
                }
            }
        }

    }
}
