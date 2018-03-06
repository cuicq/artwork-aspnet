using System.Text.RegularExpressions;

namespace Artworks.CodeRule.Generators
{
    /// <summary>
    /// 日期编号生成器解释器。
    /// </summary>
    public sealed class DateTimeCodeGeneratorInterceptor : ICodeRuleInterceptor
    {
        private static readonly Regex regex = new Regex(@"^<日期(:(?<格式>.*?))?>$");

        /// <inheritdoc />
        public bool IsMatch(string codeRule)
        {
            return regex.IsMatch(codeRule);
        }

        /// <inheritdoc />
        public ICodeGenerator Intercept(string codeRule)
        {
            var match = regex.Match(codeRule);
            var format = match.Groups["格式"].Value;
            return new DateTimeCodeGenerator(string.IsNullOrEmpty(format) ? "yyyyMMdd" : format);
        }
    }
}
