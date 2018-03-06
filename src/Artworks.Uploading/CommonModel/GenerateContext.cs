
namespace Artworks.Uploading.CommonModel
{
    /// <summary>
    /// 生成结果。
    /// </summary>
    public class GenerateContext
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public System.Drawing.Rectangle Rectangle { get; set; }

        public PictureGenerateRegistry GenerateRegistry { get; set; }
    }
}
