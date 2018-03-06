using Artworks.CodeRule.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.CodeRule
{
    /// <summary>
    /// 编号生成器解释器。
    /// </summary>
    public sealed class CodeRuleInterceptor
    {
        private readonly List<ICodeRuleInterceptor> _interceptors = new List<ICodeRuleInterceptor>();

        /// <summary>
        /// 构造方法。
        /// </summary>
        public CodeRuleInterceptor() { }

        /// <summary>
        /// 构造方法。
        /// </summary>
        public CodeRuleInterceptor(IEnumerable<ICodeRuleInterceptor> interceptors)
        {
            _interceptors.AddRange(interceptors);
        }

        /// <summary>
        /// 注册解释器。
        /// </summary>
        public CodeRuleInterceptor RegisterInterceptor(ICodeRuleInterceptor interceptor)
        {
            _interceptors.Add(interceptor);

            return this;
        }

        /// <inheritdoc />
        public bool IsMatch(string codeRule)
        {
            return codeRule.StartsWith("(") && codeRule.EndsWith(")");
        }

        /// <inheritdoc />
        public ICodeGenerator Intercept(string codeRule)
        {
            var generators = this.GetGenerators(codeRule);

            return new CompositeCodeGenerator(generators);
        }

        private IEnumerable<ICodeGenerator> GetGenerators(string codeRule)
        {
            var literals = codeRule.Replace("<", "$<").Replace(">", ">$").Split('$');

            return literals
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(this.GetGenerator)
                .ToList();
        }

        private ICodeGenerator GetGenerator(string literal)
        {
            var interceptor = _interceptors.FirstOrDefault(x => x.IsMatch(literal));

            return interceptor.Intercept(literal);
        }
    }
}
