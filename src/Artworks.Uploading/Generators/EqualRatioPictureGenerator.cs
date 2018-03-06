using Artworks.Uploading.CommonModel;
using System;

namespace Artworks.Uploading.Generators
{
    /// <summary>
    /// 等比例图片生成。
    /// </summary>
    public class EqualRatioPictureGenerator : IPictureGenerator
    {
        private int height = 0;
        private int width = 0;
        private PictureGenerateRegistry registry;

        public EqualRatioPictureGenerator(PictureGenerateRegistry registry)
        {
            this.height = registry.Height;
            this.width = registry.Width;
            this.registry = registry;
        }

        public GenerateContext Generate(GenerateArguments arguments)
        {
            int x = 0, y = 0, nx = 0, ny = 0;
            if (nx < x && ny < y)
            {
                nx = arguments.Width;
                ny = arguments.Height;
            }
            else
            {
                if ((double)arguments.Height / (double)arguments.Width > (double)this.height / (double)this.width)
                {
                    if (arguments.Height > this.height)
                    {
                        ny = this.height;
                        nx = Convert.ToInt32(Math.Round((double)this.height * arguments.Width / arguments.Height));
                    }
                    else
                    {
                        ny = arguments.Height;
                        nx = arguments.Width;
                    }
                }
                else
                {
                    if (arguments.Width > this.width)
                    {
                        nx = this.width;
                        ny = Convert.ToInt32(Math.Round((double)this.width * arguments.Height / arguments.Width));
                    }
                    else
                    {
                        ny = arguments.Height;
                        ny = arguments.Width;
                    }
                }
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
