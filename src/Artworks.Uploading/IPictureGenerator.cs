using Artworks.Uploading.CommonModel;

namespace Artworks.Uploading
{
    /// <summary>
    /// 图片生成接口。
    /// </summary>
    public interface IPictureGenerator
    {
        /// <summary>
        /// 生成图片。
        /// </summary>
        GenerateContext Generate(GenerateArguments arguments);
    }
}
