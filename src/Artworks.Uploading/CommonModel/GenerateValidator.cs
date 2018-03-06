using Artworks.Uploading.CommonModel.Internal;
using System;
using System.IO;
using System.Web;

namespace Artworks.Uploading.CommonModel
{
    /// <summary>
    /// 生成验证。
    /// </summary>
    internal class GenerateValidator
    {
        private long minSize = 0;
        private long maxSize = 0;
        private Stream inputStream;
        public Stream InputStream { get { return this.inputStream; } }

        public GenerateValidator(Tuple<int, int> size)
        {
            this.minSize = size.Item1;
            this.maxSize = size.Item2;
        }


        public Tuple<int, string> Validate(HttpContext context)
        {
            if (context.Request.HttpMethod != "POST")
            {
                return new Tuple<int, string>(ReturnMessage.Code101, ReturnMessage.GetMessage(ReturnMessage.Code101));
            }

            //添加cookie认证
            if (false)//身份认证
            {
                return new Tuple<int, string>(ReturnMessage.Code102, ReturnMessage.GetMessage(ReturnMessage.Code102));
            }

            if (context.Request.Files.Count > 0)
            {
                this.inputStream = context.Request.Files[0].InputStream;
            }
            else
            {
                if (!string.IsNullOrEmpty(context.Request.Form["data"]))
                {
                    string base64 = context.Request.Form["data"];
                    var array = base64.Split(',');
                    if (array.Length != 2)
                    {
                        return new Tuple<int, string>(ReturnMessage.Code103, ReturnMessage.GetMessage(ReturnMessage.Code103));
                    }
                    else
                    {
                        byte[] buffer = Convert.FromBase64String(array[1]);
                        this.inputStream = new MemoryStream(buffer);
                    }
                }
                else
                {
                    this.inputStream = context.Request.InputStream;
                    string filename = context.Request["file"];
                    if (string.IsNullOrEmpty(filename) && this.inputStream.Length == 0)
                    {
                        return new Tuple<int, string>(ReturnMessage.Code103, ReturnMessage.GetMessage(ReturnMessage.Code103));
                    }
                }
            }

            if (this.inputStream.Length < 0)
            {
                return new Tuple<int, string>(ReturnMessage.Code103, ReturnMessage.GetMessage(ReturnMessage.Code103));
            }

            if ((minSize > 0 && this.inputStream.Length < minSize) || (maxSize > 0 && this.inputStream.Length > maxSize))
            {
                return new Tuple<int, string>(ReturnMessage.Code104, ReturnMessage.GetMessage(ReturnMessage.Code104));
            }

            if (!Tool.IsAllowImageType(Tool.StreamToByte(this.inputStream)))
            {
                return new Tuple<int, string>(ReturnMessage.Code105, ReturnMessage.GetMessage(ReturnMessage.Code105));
            }

            return new Tuple<int, string>(ReturnMessage.Code0, ReturnMessage.GetMessage(ReturnMessage.Code0));
        }

    }
}
