using System;
using System.Data;
using System.Web;
using System.Xml;

namespace Artworks.Cache.Core
{
    /// <summary>
    ///缓存文件读取类。
    /// </summary>
    public class CacheFileReader
    {
        private static object lockObj = new object();

        /// <summary>
        /// 读取缓存文件转成DataTable
        /// </summary>
        public static DataTable ReadToDataTable(string path)
        {
            XmlDocument xml = new XmlDocument();
            #region 加载xml

            try
            {
                xml.Load(path);
            }
            catch
            {
                System.Threading.Thread.Sleep(5);
                xml.Load(path);
            }

            #endregion

            XmlNode node = xml.SelectSingleNode("data");
            if (node == null) return null;

            string content = node.InnerText.TrimEnd(',');
            string[] array = content.Split(',');

            if (array.Length > 0)
            {
                DataTable dt = new DataTable();
                string[] names = array[0].TrimEnd('|').Split('|');
                string[] types = array[1].TrimEnd('|').Split('|');

                for (var i = 0; i < names.Length; i++)
                {
                    string columnName = names[i];
                    if (!string.IsNullOrEmpty(columnName))
                    {
                        DataColumn column = new DataColumn();
                        column.ColumnName = columnName;
                        column.DataType = Type.GetType(types[i]);
                        dt.Columns.Add(column);
                    }
                }

                for (var i = 2; i < array.Length; i++)
                {
                    string[] dataItems = array[i].TrimEnd('|').Split('|');
                    DataRow row = dt.NewRow();
                    for (var j = 0; j < dataItems.Length; j++)
                    {
                        row[j] = HttpUtility.HtmlDecode(dataItems[j]).Replace("###", ",").Replace("@@@", "|");
                    }
                    dt.Rows.Add(row);
                }
                return dt;
            }
            return new DataTable();
        }

    }
}
