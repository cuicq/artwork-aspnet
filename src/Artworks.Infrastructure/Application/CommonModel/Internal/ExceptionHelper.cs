using Artworks.Anomaly;
using Artworks.Infrastructure.Application.Persistence.CommonModel;
using Artworks.Infrastructure.Application.Service.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Infrastructure.Application.CommonModel.Internal
{
    /// <summary>
    /// 异常处理帮助。
    /// </summary>
    internal class ExceptionHelper
    {
        /// <summary>
        /// 仓储异常处理
        /// </summary>
        public static bool HandleRepositoryException(Exception ex)
        {
            var innerException = new RepositoryException(ex.Message, ex);
            return ExceptionManager.HandleException(innerException);
        }

        /// <summary>
        /// 服务异常处理
        /// </summary>
        public static bool HandleServiceException(Exception ex)
        {
            var innerException = new ServiceException(ex.Message, ex);
            return ExceptionManager.HandleException(innerException);
        }

    }
}
