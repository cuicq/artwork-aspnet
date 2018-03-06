using Artworks.Infrastructure.Application.Configuration;
using System;

namespace Artworks.Infrastructure.Application.CommonModel
{
    /// <summary>
    /// 应用程序启动事件参数类。
    /// </summary>
    public class ApplicationStartupEventArgs : EventArgs
    {
        public ApplicationConfiguration Config { get; private set; }

        public ApplicationStartupEventArgs(ApplicationConfiguration config)
        {
            this.Config = config;
        }

    }
}
