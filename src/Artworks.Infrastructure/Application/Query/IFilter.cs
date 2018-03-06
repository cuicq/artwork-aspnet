using Artworks.Infrastructure.Application.Query.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Infrastructure.Application.Query
{
    /// <summary>
    /// 筛选器接口。
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// 连接器。
        /// </summary>
        Connector Connector { get; }
    }
}
