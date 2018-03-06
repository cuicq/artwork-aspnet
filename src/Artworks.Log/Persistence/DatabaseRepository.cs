using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Log.Persistence
{
    /// <summary>
    /// 数据库存储。
    /// </summary>
    public class DatabaseRepository : ILoggerRepository
    {
        public void Execute(string message, Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
