using System.Collections.Generic;
using System.Linq;

namespace Artworks.CodeRule.Generators
{
    /// <summary>
    /// 复合编号生成器。
    /// </summary>
    public sealed class CompositeCodeGenerator : ICodeGenerator
    {
        private readonly IEnumerable<ICodeGenerator> generators;

        /// <summary>
        /// 构造方法。
        /// </summary>
        public CompositeCodeGenerator(IEnumerable<ICodeGenerator> generators)
        {
            Guard.ArgumentNotNull(generators, "generators");
            this.generators = generators;
        }

        /// <inheritdoc />
        public string Generate(GenerateContext context)
        {
            var codes = generators.Select(x => x.Generate(context)).ToArray();
            return string.Join(string.Empty, codes);
        }
    }
}
