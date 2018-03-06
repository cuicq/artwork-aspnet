using Artworks.Infrastructure.Application.CommonModel;
using Artworks.Uploading.CommonModel;
using Artworks.Uploading.CommonModel.Internal;
using Artworks.Uploading.Generators;
using Artworks.Utility.IO;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;

namespace Artworks.Uploading
{
    /// <summary>
    /// 图片生成器。
    /// </summary>
    internal class PictureGenerator
    {
        /// <summary>
        /// 生成图片
        /// </summary>
        public static ResponseResult Generate(Stream stream)
        {
            ResponseResult result = new ResponseResult();

            using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream))
            {
                var aguments = new GenerateArguments() { Width = image.Width, Height = image.Height };

                var list = CompositePictureGenerator.Instance.Generate(aguments);

                foreach (GenerateContext context in list)
                {
                    var registry = context.GenerateRegistry;
                    var rectangle = context.Rectangle;

                    using (Bitmap bitmap = Tool.GetCanvasSize(registry, rectangle))
                    {
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                            if (registry.IsClearRectangle)
                            {
                                graphics.Clear(Color.White);
                            }

                            #region 裁剪

                            if (registry.IsCut)
                            {
                                graphics.DrawImage(image, new Rectangle(0, 0, registry.Width, registry.Height), rectangle, GraphicsUnit.Pixel);
                            }
                            else
                            {
                                graphics.DrawImage(image, rectangle, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                            }

                            #endregion

                            #region 水印

                            if (registry.IsWaterMark)
                            {
                                if (Enum.IsDefined(typeof(WaterMarkType), registry.WaterMarkType))
                                {
                                    WaterMarkType waterMarkType = (WaterMarkType)Enum.Parse(typeof(WaterMarkType), registry.WaterMarkType);

                                    if (waterMarkType == WaterMarkType.Text)
                                    {
                                        if (Enum.IsDefined(typeof(FontStyle), registry.FontStyle))
                                        {
                                            using (Font textFont = new Font(registry.FontName, registry.FontSize, (FontStyle)Enum.Parse(typeof(FontStyle), registry.FontStyle)))
                                            {
                                                graphics.DrawString(registry.WaterMarkText, textFont, Brushes.Black, bitmap.Width - registry.FontButtomX, bitmap.Height - registry.FontButtomY);
                                                graphics.DrawString(registry.WaterMarkText, textFont, Brushes.White, registry.FontTopX, registry.FontTopY);
                                            }
                                        }
                                        else
                                        {
                                            result.Message = "压缩字体配置错误";
                                        }
                                    }
                                }
                                else
                                {
                                    result.Message = "水印类型错误";
                                }
                            }

                            #endregion

                            #region 存储图片

                            string file = string.Format("{0}.jpg", Tool.GenerateID());

                            var date = DateTime.Now;

                            string path = registry.Path.ToLower().Replace("{yyyy}", date.ToString("yyyy"))
                                   .Replace("{mm}", date.ToString("MM"))
                                   .Replace("{dd}", date.ToString("dd"))
                                   .Replace("{filename}", file);

                            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                            string dir = baseDir + path;

                            DirectoryUtil.CreateDirectoryIfNotExists(Path.GetDirectoryName(Tool.AbsolutePath(dir)));

                            using (EncoderParameters encoderParameters = Tool.InitEncoderParameters(registry.Quality))
                            {
                                bitmap.Save(Tool.AbsolutePath(path), Tool.GetEncoderInfo("image/jpeg"), encoderParameters);

                                if (registry.Name == 0)
                                {
                                    Regex regex = new Regex(@"\w+\.\w+\.com");
                                    var match = regex.Match(path);
                                    if (match.Success)
                                    {
                                        string domain = match.Value;
                                        string temp = path.Substring(path.IndexOf(domain) + domain.Length);

                                        result.Message = temp;
                                        result.Data = string.Format("http://{0}/{1}", domain, temp);
                                        result.Status = 0;
                                    }
                                }

                            }

                            #endregion



                        }
                    }

                }

                return null;
            }
        }
    }
}
