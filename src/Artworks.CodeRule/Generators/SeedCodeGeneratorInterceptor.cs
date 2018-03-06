using System;
using System.Text.RegularExpressions;

namespace Artworks.CodeRule.Generators
{
    /// <summary>
    /// 种子编号生成器解释器。
    /// </summary>
    public sealed class SeedCodeGeneratorInterceptor : ICodeRuleInterceptor
    {
        private static readonly Regex regex = new Regex(@"^<种子:(?<键格式>.*?)(:(?<宽度>\d*?))?>$");
        private readonly ISeedStore _seedStore;

        /// <summary>
        /// 构造方法。
        /// </summary>
        public SeedCodeGeneratorInterceptor(ISeedStore seedStore)
        {
            _seedStore = seedStore;
        }

        /// <inheritdoc />
        public bool IsMatch(string codeRule)
        {
            return regex.IsMatch(codeRule);
        }

        /// <inheritdoc />
        public ICodeGenerator Intercept(string codeRule)
        {
            var match = regex.Match(codeRule);

            var seedKeyFormat = match.Groups["键格式"].Value;
            var seedWidthString = match.Groups["宽度"].Value;

            var seedKey = DateTime.Now.ToString(seedKeyFormat);
            int seedWidth = string.IsNullOrEmpty(seedWidthString) ? 5 : int.Parse(seedWidthString);

            return new SeedCodeGenerator(_seedStore, seedKey, seedWidth);
        }
    }
}
