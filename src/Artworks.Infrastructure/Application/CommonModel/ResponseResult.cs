
using Artworks.Utility.Common;
namespace Artworks.Infrastructure.Application.CommonModel
{
    /// <summary>
    /// 响应结果。
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>

        public object Data { get; set; }

        /// <summary>
        /// 输出json字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string json = SerializeUtil.ObjectToJSON(new
            {
                status = this.Status,
                message = this.Message,
                result = this.Data
            });
            return json;
        }
    }
}
