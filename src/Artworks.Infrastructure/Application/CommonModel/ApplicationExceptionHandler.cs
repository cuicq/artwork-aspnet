using Artworks.Anomaly;
using Artworks.Log;
using System;

namespace Artworks.Infrastructure.Application.CommonModel
{
    /// <summary>
    /// 表示该类为基础设施异常处理接口实现类
    /// </summary>
    public class ApplicationExceptionHandler : ExceptionHandler<ApplicationException>
    {
        protected override bool Execute(ApplicationException ex)
        {
            LogHelper.Debug("Artworks.Infrastructure.Application框架应用程序异常", ex);
            return true;
        }
    }
}
