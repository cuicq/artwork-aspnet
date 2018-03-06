using System.Web.Script.Serialization;

namespace Artworks.Utility.Common
{
    /// <summary>
    /// 序列化。
    /// </summary>
    public class SerializeUtil
    {
        /// <summary>
        /// 对象转JSON
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>JSON格式的字符串</returns>
        public static string ObjectToJSON(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jss.Serialize(obj);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        /// <summary>
        /// JSON文本转对象,泛型方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="data">JSON文本</param>
        /// <returns>指定类型的对象</returns>
        public static T JSONToObject<T>(string data)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jss.Deserialize<T>(data);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }


    }
}
