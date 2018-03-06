using System;
using System.Drawing;
using System.Web;

namespace Artworks.Utility.Common
{
    /// <summary>
    /// 验证码类。
    /// </summary>
    public class VerifyCode
    {
        #region 验证码参数
        /// <summary>
        /// 验证码长度(默认4个验证码的长度)
        /// </summary>
        int length = 4;
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        /// <summary>
        /// 验证码字体大小(默认12像素)
        /// </summary>
        int fontSize = 16;
        public int FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }
        /// <summary>
        /// 边框补(默认2像素)
        /// </summary>
        int padding = 2;
        public int Padding
        {
            get { return padding; }
            set { padding = value; }
        }
        /// <summary>
        /// 是否输出燥点(默认不输出)
        /// </summary>
        bool chaos = false;
        public bool Chaos
        {
            get { return chaos; }
            set { chaos = value; }
        }
        /// <summary>
        /// 输出燥点的颜色(默认灰色)
        /// </summary>
        Color chaosColor = Color.LightGray;
        public Color ChaosColor
        {
            get { return chaosColor; }
            set { chaosColor = value; }
        }
        /// <summary>
        /// 自定义背景色(默认白色)
        /// </summary>
        Color backgroundColor = Color.White;
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }
        /// <summary>
        /// 自定义随机颜色数组
        /// </summary>
        Color[] colors = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        public Color[] Colors
        {
            get { return colors; }
            set { colors = value; }
        }
        /// <summary>
        /// 自定义字体数组
        /// </summary>
        string[] fonts = { "Arial", "Georgia", "微软雅黑" };
        public string[] Fonts
        {
            get { return fonts; }
            set { fonts = value; }
        }
        /// <summary>
        /// 自定义随机码字符串序列(使用逗号分隔)
        /// </summary>
        //string codeSerial = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
        protected string codeSerial = "2,3,4,6,7,8,9,a,b,c,d,e,f,g,h,j,k,m,n,p,q,r,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,T,U,V,W,X,Y,Z";
        public string CodeSerial
        {
            get { return codeSerial; }
            set { codeSerial = value; }
        }
        #endregion

        #region 生成校验码图片
        /// <summary>
        /// 生成校验码图片
        /// </summary>
        /// <param name="code">已经生成的随机码</param>
        /// <returns>位图</returns>
        public Bitmap CreateImageCode(string code)
        {
            int fSize = FontSize;
            int fWidth = fSize + Padding;
            int imageWidth = (int)(code.Length * fWidth) + 4 + Padding * 2;
            int imageHeight = fSize * 2 + Padding;

            System.Drawing.Bitmap image = new System.Drawing.Bitmap(imageWidth, imageHeight);
            Graphics g = Graphics.FromImage(image);
            g.Clear(BackgroundColor);
            Random rand = new Random();
            //给背景添加随机生成的燥点
            if (this.Chaos)
            {
                Pen pen = new Pen(ChaosColor, 0);
                int c = Length * 10;
                for (int i = 0; i < c; i++)
                {
                    int x = rand.Next(image.Width);
                    int y = rand.Next(image.Height);
                    g.DrawRectangle(pen, x, y, 1, 1);
                }
            }
            int left = 0, top = 0, top1 = 1, top2 = 1;
            int n1 = (imageHeight - FontSize - Padding * 2);
            int n2 = n1 / 4;
            top1 = n2;
            top2 = n2 * 2;
            Font f;
            Brush b;
            int cindex, findex;
            //随机字体和颜色的验证码字符
            for (int i = 0; i < code.Length; i++)
            {
                cindex = rand.Next(Colors.Length - 1);
                findex = rand.Next(Fonts.Length - 1);
                f = new System.Drawing.Font(Fonts[findex], fSize, System.Drawing.FontStyle.Bold);
                b = new System.Drawing.SolidBrush(Colors[cindex]);
                if (i % 2 == 1)
                {
                    top = top2;
                }
                else
                {
                    top = top1;
                }
                left = i * fWidth;
                g.DrawString(code.Substring(i, 1), f, b, left, top);
            }

            //画一个边框
            // g.DrawRectangle(new Pen(Color.Silver, 0), 0, 0, image.Width - 1, image.Height - 1);
            g.Dispose();
            return image;
        }
        #endregion

        #region 将创建好的图片输出到页面
        /// <summary>
        /// 将创建好的图片输出到页面
        /// </summary>
        /// <param name="code">已经生成的随机码</param>
        /// <param name="context">Web上下文</param>
        public void CreateImageOnPage(string code, HttpContext context)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            Bitmap image = this.CreateImageCode(code);
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            context.Response.ClearContent();
            context.Response.ContentType = "image/Jpeg";
            context.Response.BinaryWrite(ms.GetBuffer());
            ms.Close();
            ms = null;
            image.Dispose();
            image = null;
        }
        #endregion

        #region 生成随机字符码
        /// <summary>
        /// 生成随机字符码
        /// </summary>
        /// <param name="codeLen">随机码长度</param>
        /// <returns>产生的随机码</returns>
        public string CreateVerifyCode(int codeLen)
        {
            if (codeLen == 0)
            {
                codeLen = Length;
            }
            string[] arr = CodeSerial.Split(',');
            string code = "";
            int randValue = -1;
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, arr.Length - 1);
                code += arr[randValue];
            }
            return code;
        }
        public string CreateVerifyCode()
        {
            return CreateVerifyCode(0);
        }
        #endregion
    }
}
