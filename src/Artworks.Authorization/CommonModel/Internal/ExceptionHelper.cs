using Artworks.Anomaly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Authorization.CommonModel.Internal
{
    /// <summary>
    /// 异常处理帮助。
    /// </summary>
    internal class ExceptionHelper
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        public static bool HandleException(Exception ex)
        {
            var innerException = new AuthorizationException(ex.Message, ex);
            return ExceptionManager.HandleException(innerException);
        }

    }
}
