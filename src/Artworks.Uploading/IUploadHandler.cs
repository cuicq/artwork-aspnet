using Artworks.Infrastructure.Application.CommonModel;
using System.IO;

namespace Artworks.Uploading
{
    /// <summary>
    /// 上传文件接口。
    /// </summary>
    public interface IUploadHandler
    {
        /// <summary>
        /// 执行
        /// </summary>
        ResponseResult Execute(Stream stream);
    }
}
