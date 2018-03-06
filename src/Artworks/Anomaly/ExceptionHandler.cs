using System;

namespace Artworks.Anomaly
{
    /// <summary>
    /// 异常信息处理接口基类。
    /// </summary>
    public abstract class ExceptionHandler<TException> : IExceptionHandler where TException : System.Exception
    {
        /// <summary>
        /// 执行异常处理
        /// </summary>
        protected abstract bool Execute(TException ex);

        /// <summary>
        /// 异常处理
        /// </summary>
        public virtual bool Handle(Exception ex)
        {
            return this.Execute(ex as TException);
        }
    }
}
