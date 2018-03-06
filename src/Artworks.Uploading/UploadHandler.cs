using Artworks.Infrastructure.Application.CommonModel;
using Artworks.Infrastructure.Application.Web;
using Artworks.Uploading.CommonModel;
using Artworks.Uploading.CommonModel.Internal;
using System;

namespace Artworks.Uploading
{
    /// <summary>
    /// 上传文件接口。
    /// </summary>
    public abstract class UploadHandler : WebHandler, IUploadHandler
    {

        protected override void ProcessRequest()
        {
            this.ProcessUpload();
        }

        protected virtual void ProcessUpload()
        {
            this.Context.Response.ContentType = "text/html; charset=gb2312";
            this.Context.Response.CacheControl = "no-cache";

            ResponseResult result = new ResponseResult();
            try
            {
                GenerateValidator validator = new GenerateValidator(new Tuple<int, int>(0, 5 * 1024 * 1024));
                Tuple<int, string> validResult = validator.Validate(this.Context);

                //成功
                if (validResult.Item1 == ReturnMessage.Code0)
                {
                    result = this.Execute(validator.InputStream);
                }
                else
                {
                    result.Status = validResult.Item1;
                    result.Message = validResult.Item2;
                }
            }
            catch (Exception ex)
            {
                result.Status = ReturnMessage.Code500;
                result.Message = ReturnMessage.GetMessage(ReturnMessage.Code500);
            }

            base.Output(result);
        }


        public virtual ResponseResult Execute(System.IO.Stream stream)
        {
            return PictureGenerator.Generate(stream);
        }
    }
}
