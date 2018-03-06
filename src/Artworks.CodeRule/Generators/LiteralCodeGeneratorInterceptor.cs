using System.Text.RegularExpressions;

namespace Artworks.CodeRule.Generators
{
    /// <summary>
    /// 字面量编号生成器解释器。
    /// </summary>
    public sealed class LiteralCodeGeneratorInterceptor : ICodeRuleInterceptor
    {
        private static readonly Regex regex = new Regex(@"^[^<\(].*?[^>\)]?$");

        /// <inheritdoc />
        public bool IsMatch(string codeRule)
        {
            return regex.IsMatch(codeRule);
        }

        /// <inheritdoc />
        public ICodeGenerator Intercept(string codeRule)
        {
            return new LiteralCodeGenerator(codeRule);
        }
    }
}
