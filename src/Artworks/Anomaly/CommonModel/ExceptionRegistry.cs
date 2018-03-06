using System.Collections.Generic;

namespace Artworks.Anomaly.CommonModel
{
    /// <summary>
    /// 异常信息。
    /// </summary>
    public class ExceptionRegistry
    {
        /// <summary>
        /// 类型。
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 异常处理方式。
        /// </summary>
        public ExceptionHandlingBehavior Behavior { get; set; }

        /// <summary>
        /// 异常处理集。
        /// </summary>
        public List<ExceptionHandlerType> Handlers { get; set; }

        public ExceptionRegistry()
        {
            this.Behavior = ExceptionHandlingBehavior.Direct;
            this.Handlers = new List<ExceptionHandlerType>();
        }
    }
}
