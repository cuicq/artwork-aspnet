using Artworks.Uploading.CommonModel;
using System;

namespace Artworks.Uploading.Generators
{
    /// <summary>
    /// 裁剪图片生成。
    /// </summary>
    public class TailorPictureGenerator : IPictureGenerator
    {
        private int height = 0;
        private int width = 0;
        private PictureGenerateRegistry registry;

        public TailorPictureGenerator(PictureGenerateRegistry registry)
        {
            this.height = registry.Height;
            this.width = registry.Width;
            this.registry = registry;
        }

        public GenerateContext Generate(GenerateArguments arguments)
        {
            int x = 0, y = 0, nx = 0, ny = 0;

            if ((double)arguments.Width / (double)arguments.Height > (double)this.width / (double)this.height)
            {
                ny = arguments.Height;
                nx = arguments.Height * this.width / this.height;
                y = 0;
                x = (arguments.Width - nx) / 2;
            }
            else
            {
                nx = arguments.Width;
                ny = Convert.ToInt32(Math.Round((double)arguments.Width * this.height / this.width));
                x = 0;
                y = (arguments.Height - ny) / 2;
            }

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
