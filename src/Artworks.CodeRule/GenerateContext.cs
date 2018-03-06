using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.CodeRule
{
    /// <summary>
    /// 编号生成上下文。
    /// </summary>
    public sealed class GenerateContext
    {
        /// <summary>
        /// 单据目标。
        /// </summary>
        public object Target { get; set; }
    }
}
