using Artworks.Anomaly;
using Artworks.Database.CommonModel;
using Artworks.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Examples.Codes.Exception.Code
{
    public class DatabaseExceptionHandler : ExceptionHandler<DatabaseException>
    {
        protected override bool Execute(DatabaseException ex)
        {
            LogHelper.Debug("execute sql", ex);
            return true;
        }
    }
}
