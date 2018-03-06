using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.CodeRule
{
    /// <summary>
    /// 编码规则解释器接口。
    /// </summary>
    public interface ICodeRuleInterceptor
    {
        /// <summary>
        /// 是否匹配。
        /// </summary>
        bool IsMatch(string codeRule);

        /// <summary>
        /// 执行解析。
        /// </summary>
        ICodeGenerator Intercept(string codeRule);
    }
}
