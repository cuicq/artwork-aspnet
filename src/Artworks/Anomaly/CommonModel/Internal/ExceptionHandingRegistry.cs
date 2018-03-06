using System.Collections.Generic;

namespace Artworks.Anomaly.CommonModel.Internal
{
    /// <summary>
    /// 异常处理注册。
    /// </summary>
    internal class ExceptionHandingRegistry
    {
        public ExceptionHandlingBehavior Behavior { get; set; }
        public List<IExceptionHandler> Handlers { get; set; }

        public ExceptionHandingRegistry()
        {
            this.Behavior = ExceptionHandlingBehavior.Direct;
            this.Handlers = new List<IExceptionHandler>();
        }

    }
}
