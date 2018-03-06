using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web;

namespace Artworks.Uploading.CommonModel.Internal
{
    /// <summary>
    /// 工具类。
    /// </summary>
    internal class Tool
    {
        #region 生成唯一ID
        /// <summary>
        /// 生成唯一ID
        /// </summary>
        /// <returns></returns>
        public static long GenerateID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        #endregion

        #region 针对中文Unicode编码

        /// <summary>
        /// 针对中文Unicode编码
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <returns>返回编码后的字符串</returns>
        public static string UnicodeEncode(string str)
        {
            StringBuilder outStr = new StringBuilder();
            if (string.IsNullOrWhiteSpace(str) == false)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    char chars = str[i];
                    if ((int)chars > 255)
                    {
                        //将双字节字符转为10进制整数，然后转为16进制unicode字符
                        outStr.Append(@"\u" + ((int)chars).ToString("x"));
                    }
                    else
                    {
                        outStr.Append(chars);
                    }
                }
            }
            return outStr.ToString();
        }

        #endregion

        #region 是否允许的图片类型

        /// <summary>
        /// 是否允许的图片类型
        /// </summary>
        public static bool IsAllowImageType(byte[] buffer)
        {
            if (buffer == null || buffer.Length < 3)
                return false;
            string type = buffer[0].ToString() + buffer[1].ToString();
            bool isPic = false;
            switch (type)
            {
                case "255216"://jpg
                    isPic = true;
                    break;
                case "7173"://gif
                    isPic = true;
                    break;
                case "13780"://png
                    isPic = true;
                    break;
            }
            return isPic;
        }

        #endregion

        #region 流转字节
        /// <summary>
        /// 流转字节
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <returns></returns>
        public static byte[] StreamToByte(Stream stream)
        {
            byte[] buf = null;
            try
            {
                if (stream != null)
                {
                    buf = new byte[stream.Length];
                    stream.Read(buf, 0, buf.Length);
                }
            }
            catch { stream.Dispose(); }

            return buf;
        }
        #endregion

        #region 获取画布

        /// <summary>
        /// 获取画布
        /// </summary>
        /// <returns></returns>
        public static Bitmap GetCanvasSize(PictureGenerateRegistry registry, Rectangle rectangle)
        {
            Bitmap bitmap = null;

            GenerateType generateType = GenerateType.Origin;
            Enum.TryParse<GenerateType>(registry.GenerateType, out generateType);

            switch (generateType)
            {
                case GenerateType.EqualRatio:
                    bitmap = new Bitmap(rectangle.Width, rectangle.Height);
                    break;
                case GenerateType.HeightSide:
                    bitmap = new Bitmap(registry.Name, rectangle.Height);
                    break;
                /*
            case CompressType.Scale:
                bitmap = new Bitmap(rule.Name, (rule.ScaleX * rule.Name) / rule.ScaleY);
                break;
                 * */
                case GenerateType.Tailor:
                    bitmap = new Bitmap(registry.Width, registry.Height);
                    break;
            }
            return bitmap;
        }

        #endregion

        #region 获取图片上传绝对路径
        /// <summary>
        /// 获取图片上传绝对路径
        /// </summary>
        /// <param name="path">配置文件里图片地址</param>
        /// <returns></returns>
        public static string AbsolutePath(string path)
        {
            if (path.IndexOf("~", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                return HttpContext.Current.Server.MapPath(path);
            }
            else
            {
                return path;
            }
        }
        #endregion

        #region 图片压缩参数
        /// <summary>
        /// 图片压缩参数
        /// </summary>
        /// <param name="quality">图片质量百分比</param>
        /// <returns></returns>
        public static EncoderParameters InitEncoderParameters(int quality)
        {
            EncoderParameters objEncoderParameters = new EncoderParameters(1);
            System.Drawing.Imaging.Encoder objEncoder0 = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameter objEncoderParameter0 = new EncoderParameter(objEncoder0, quality);
            objEncoderParameters.Param[0] = objEncoderParameter0;
            return objEncoderParameters;
        }
        #endregion

        #region 图片信息
        /// <summary>
        /// 图片信息
        /// </summary>
        /// <param name="mimeType">图片类型</param>
        /// <returns></returns>
        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        #endregion

    }
}
