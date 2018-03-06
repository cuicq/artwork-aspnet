using System.Text.RegularExpressions;

namespace Artworks.CodeRule.Generators
{
    /// <summary>
    /// 属性编号生成器解释器。
    /// </summary>
    public sealed class PropertyCodeGeneratorInterceptor : ICodeRuleInterceptor
    {
        private static readonly Regex regex = new Regex(@"^<属性:(?<名字>.*?)>$");

        /// <inheritdoc />
        public bool IsMatch(string codeRule)
        {
            return regex.IsMatch(codeRule);
        }

        /// <inheritdoc />
        public ICodeGenerator Intercept(string codeRule)
        {
            var match = regex.Match(codeRule);

            var property = match.Groups["名字"].Value;

            return new PropertyCodeGenerator(property);
        }
    }
}
