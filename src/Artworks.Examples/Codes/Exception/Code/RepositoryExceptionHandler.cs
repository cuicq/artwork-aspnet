using Artworks.Anomaly;
using Artworks.Infrastructure.Application.Persistence.CommonModel;
using Artworks.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Examples.Codes.Exception.Code
{
    public class RepositoryExceptionHandler : ExceptionHandler<RepositoryException>
    {
        protected override bool Execute(RepositoryException ex)
        {
            LogHelper.Debug(ex.Message);
            return true;
        }
    }
}
