
namespace Artworks.CodeRule.Generators
{
    /// <summary>
    /// 属性编号生成器。
    /// </summary>
    public sealed class PropertyCodeGenerator : ICodeGenerator
    {
        private readonly string _property;

        /// <summary>
        /// 构造方法。
        /// </summary>
        public PropertyCodeGenerator(string property)
        {
            _property = property;
        }

        /// <inheritdoc />
        public string Generate(GenerateContext context)
        {
            return context
                .Target
                .GetType()
                .GetProperty(_property)
                .GetValue(context.Target, null)
                .ToString();
        }
    }
}
