using System.Collections.Generic;

namespace Artworks.Uploading.CommonModel.Internal
{

    /// <summary>
    /// 返回消息
    /// </summary>
    public class ReturnMessage
    {
        public const int Code0 = 0;
        public const int Code1 = 1;
        public const int Code2 = 2;

        public const int Code101 = 101;
        public const int Code102 = 102;
        public const int Code103 = 103;
        public const int Code104 = 104;
        public const int Code105 = 105;
        public const int Code500 = 500;

        public const int Code201 = 201;

        private static Dictionary<int, string> dic = new Dictionary<int, string>()
        {
            {0,"失败"},
            {1,"成功"},
            {101,"非POST请求"},
            {102,"用户验证失败"},
            {103,"未获取到图片信息"},
            {104,"图片大小超出限制"},
            {105,"图片类型不正确"},
            {Code500,"服务器内部错误"}
        };

        /// <summary>
        /// 获取消息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetMessage(int code)
        {
            if (dic.ContainsKey(code))
            {
                return Tool.UnicodeEncode(dic[code]);
            }
            return string.Empty;
        }
    }
}
