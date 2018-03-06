using Artworks.Uploading.CommonModel;
using System;


namespace Artworks.Uploading.Generators
{
    /// <summary>
    /// 图片宽固定，按高生成。
    /// </summary>
    public class HeightSidePictureGenerator : IPictureGenerator
    {
        private int height = 0;
        private int width = 0;
        private PictureGenerateRegistry registry;

        public HeightSidePictureGenerator(PictureGenerateRegistry registry)
        {
            this.height = registry.Height;
            this.width = registry.Width;
            this.registry = registry;
        }

        public GenerateContext Generate(GenerateArguments arguments)
        {
            int x = 0, y = 0, nx = 0, ny = 0;

            nx = this.width;
            ny = Convert.ToInt32(Math.Round((double)arguments.Height * this.width / arguments.Width));

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
