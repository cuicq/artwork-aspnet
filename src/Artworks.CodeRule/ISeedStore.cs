using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.CodeRule
{
    /// <summary>
    /// 种子仓储接口。
    /// </summary>
    public interface ISeedStore
    {
        /// <summary>
        /// 自增，并返回下一个种子值。
        /// </summary>
        int NextSeed(string seedKey);
    }
}
