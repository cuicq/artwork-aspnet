using Artworks.Anomaly;
using Artworks.Infrastructure.Application.Service.CommonModel;
using Artworks.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Examples.Codes.Exception.Code
{
    public class ServiceExceptionHandler : ExceptionHandler<ServiceException>
    {
        protected override bool Execute(ServiceException ex)
        {
            LogHelper.Debug(ex.Message);
            return true;
        }
    }
}
