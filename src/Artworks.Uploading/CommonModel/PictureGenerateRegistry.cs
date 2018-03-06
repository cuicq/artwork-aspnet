using System.ComponentModel;

namespace Artworks.Uploading.CommonModel
{
    /// <summary>
    /// 图片生成注册。
    /// </summary>
    public class PictureGenerateRegistry
    {
        /// <summary>
        /// 图片尺寸名称
        /// </summary>
        public int Name { get; set; }
        /// <summary>
        /// 图片宽
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 图片高
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 生成类型
        /// </summary>
        public string GenerateType { get; set; }

        /// <summary>
        /// 压缩类型
        /// </summary>
        public string CompressType { get; set; }

        /// <summary>
        /// 是否剪切
        /// </summary>
        public bool IsCut { get; set; }

        /// <summary>
        /// 是否清空画布
        /// </summary>
        public bool IsClearRectangle { get; set; }

        /// <summary>
        /// 图片压缩比
        /// </summary>
        public int Quality { get; set; }


        #region 水印字体设置

        /// <summary>
        /// 是否水印
        /// </summary>
        public bool IsWaterMark { get; set; }

        /// <summary>
        /// 水印文字
        /// </summary>
        public string WaterMarkText { get; set; }

        /// <summary>
        /// 水印类型
        /// </summary>
        public string WaterMarkType { get; set; }

        /// <summary>
        /// 字体
        /// </summary>
        [DefaultValueAttribute("宋体")]
        public string FontName { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        [DefaultValueAttribute(9)]
        public int FontSize { get; set; }
        /// <summary>
        /// 字体样式
        /// </summary>
        [DefaultValueAttribute("Regular")]
        public string FontStyle { get; set; }

        /// <summary>
        /// 顶部横坐标
        /// </summary>
        public float FontTopX { get; set; }

        /// <summary>
        /// 顶部纵坐标
        /// </summary>
        public float FontTopY { get; set; }

        /// <summary>
        /// 距离右侧距离
        /// </summary>
        public float FontButtomX { get; set; }

        /// <summary>
        /// 距离底部距离
        /// </summary>
        public float FontButtomY { get; set; }

        #endregion
    }
}
