using Artworks.Uploading.CommonModel;

namespace Artworks.Uploading.Generators
{
    /// <summary>
    /// 原图片生成。
    /// </summary>
    internal class PictureGenerator : IPictureGenerator
    {
        private int height = 0;
        private int width = 0;
        private PictureGenerateRegistry registry;

        public PictureGenerator(PictureGenerateRegistry registry)
        {
            this.height = registry.Height;
            this.width = registry.Width;
            this.registry = registry;
        }

        public GenerateContext Generate(GenerateArguments arguments)
        {
            int x = 0, y = 0, nx = 0, ny = 0;
            nx = arguments.Width;
            ny = arguments.Height;

            return new GenerateContext()
            {
                Height = this.height,
                Width = this.width,
                Rectangle = new System.Drawing.Rectangle(x, y, nx, ny),
                GenerateRegistry = this.registry
            };
        }
    }
}
