
namespace Artworks.CodeRule.Generators
{
    /// <summary>
    /// 字面量编号生成器。
    /// </summary>
    public sealed class LiteralCodeGenerator : ICodeGenerator
    {
        private readonly string _litial;

        /// <summary>
        /// 构造方法。
        /// </summary>
        public LiteralCodeGenerator(string litial)
        {
            _litial = litial;
        }

        /// <inheritdoc />
        public string Generate(GenerateContext context)
        {
            return _litial;
        }
    }
}
