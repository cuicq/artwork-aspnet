using Artworks.Configuration;
using Artworks.Configuration.CommonModel;
using Artworks.Uploading.CommonModel;
using Artworks.Utility.Common;
using System.Xml;

namespace Artworks.Uploading.Configuration.Internal
{
    /// <summary>
    /// 图片生成配置。
    /// </summary>
    internal class PictureGenerateRegistryConfiguration : ConfigurationBase
    {
        #region 单例实例

        private static object lockObj = new object();
        private static PictureGenerateRegistryConfiguration instanceObj = null;

        /// <summary>
        /// 实例
        /// </summary>
        public static PictureGenerateRegistryConfiguration Instance
        {
            get
            {
                if (instanceObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceObj == null)
                        {
                            instanceObj = new PictureGenerateRegistryConfiguration();
                        }
                    }
                }
                return instanceObj;
            }
        }

        #endregion

        public PictureGenerateRegistryConfiguration()
            : base("upload.xml")
        {

        }

        protected override void Execute(ConfigurationContext context)
        {
            var doc = context.Config.Build();
            var defaultNodes = doc.SelectNodes("/configuration/rules/item");

            foreach (XmlNode node in defaultNodes)
            {
                PictureGenerateRegistry registry = new PictureGenerateRegistry();

                registry.Name = TypeUtil.GetInt(node.SelectSingleNode("name").InnerText, 0);
                registry.Width = TypeUtil.GetInt(node.SelectSingleNode("width").InnerText, 0);
                registry.Height = TypeUtil.GetInt(node.SelectSingleNode("height").InnerText, 0);
                registry.Quality = TypeUtil.GetInt(node.SelectSingleNode("quality").InnerText, 0);
                registry.Path = node.SelectSingleNode("path").InnerText;
                registry.IsCut = bool.Parse(node.SelectSingleNode("isCut").InnerText);
                registry.IsClearRectangle = bool.Parse(node.SelectSingleNode("clear").InnerText);
                registry.GenerateType = node.SelectSingleNode("generatetype").InnerText;
                registry.CompressType = node.SelectSingleNode("compresstype").InnerText;

                registry.IsWaterMark = bool.Parse(node.SelectSingleNode("iswaterMark").InnerText);
                if (registry.IsWaterMark)
                {
                    registry.WaterMarkType = node.SelectSingleNode("waterMark/type").InnerText;
                    registry.WaterMarkText = node.SelectSingleNode("waterMark/text").InnerText;
                    registry.FontName = node.SelectSingleNode("waterMark/fontName").InnerText;
                    registry.FontSize = TypeUtil.GetInt(node.SelectSingleNode("waterMark/fontSize").InnerText, 0);
                    registry.FontStyle = node.SelectSingleNode("waterMark/fontStyle").InnerText;
                    registry.FontTopX = TypeUtil.GetInt(node.SelectSingleNode("waterMark/fontTopX").InnerText, 0);
                    registry.FontTopY = TypeUtil.GetInt(node.SelectSingleNode("waterMark/fontTopY").InnerText, 0);
                    registry.FontButtomX = TypeUtil.GetInt(node.SelectSingleNode("waterMark/fontButtomX").InnerText, 0);
                    registry.FontButtomY = TypeUtil.GetInt(node.SelectSingleNode("waterMark/fontButtomY").InnerText, 0);
                }
                string key = registry.GenerateType;
                this.Context.Add(key, registry);


            }

        }
    }
}
